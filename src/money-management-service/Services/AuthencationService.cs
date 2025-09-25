using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using money_management_service.Data;
using money_management_service.DTOs.User;
using money_management_service.Entities.Auth;
using money_management_service.Entities.Users;
using money_management_service.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace money_management_service.Services
{
    public class AuthencationService : IAuthencationService
    {
        private ApplicationDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthencationService (ApplicationDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<TokenResponseDTO> Login(string username, string password)
        {
            var user = await _dbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(user => user.UserName == username);

            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            var token = await GenerateJwtToken(user);

            var refreshToken = GenerateRefreshToken();
            var hashedRefreshToken = HashToken(refreshToken);
            var refreshTokenEntity = new RefreshToken
            {
                Token = hashedRefreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddHours(2),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            };

            _dbContext.RefreshTokens.Add(refreshTokenEntity);
            await _dbContext.SaveChangesAsync();

            return new TokenResponseDTO
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResponseDTO> RefreshToken(RefreshTokenRequestDTO requestDto)
        {
            var hashedToken = HashToken(requestDto.RefreshToken);
            var storedRefreshToken = await _dbContext.RefreshTokens
              .Include(rt => rt.User)
              .ThenInclude(u => u.Roles)
              .FirstOrDefaultAsync(rt => rt.Token == hashedToken);

            if (storedRefreshToken == null)
            {
                return null;
            }

            if (storedRefreshToken.IsRevoked)
            {
                return null;
            }

            if (storedRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                return null;
            }

            // Retrieve the user and client
            var user = storedRefreshToken.User;
            // The existing refresh token is marked as revoked to prevent reuse.
            storedRefreshToken.IsRevoked = true;
            storedRefreshToken.RevokedAt = DateTime.UtcNow;
            // Generate a new refresh token
            var newRefreshToken = GenerateRefreshToken();
            var hashedNewRefreshToken = HashToken(newRefreshToken);
            var newRefreshTokenEntity = new RefreshToken
            {
                Token = hashedNewRefreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddHours(2),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            };

            _dbContext.RefreshTokens.Add(newRefreshTokenEntity);
            var newJwtToken = await GenerateJwtToken(user);
            await _dbContext.SaveChangesAsync();

            return new TokenResponseDTO
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task<string> LogOut(RefreshTokenRequestDTO requestDto, ClaimsPrincipal userClaims)
        {
            var userIdClaim = userClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return "Invalid access token.";
            }

            if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return "Invalid user ID in access token.";
            }

            var hashedToken = HashToken(requestDto.RefreshToken);
            var storedRefreshToken = await _dbContext.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == hashedToken && rt.UserId == userId);

            if (storedRefreshToken == null)
            {
                return "Invalid refresh token.";
            }

            if (storedRefreshToken.IsRevoked)
            {
                return "Refresh token is already revoked.";
            }

            storedRefreshToken.IsRevoked = true;
            storedRefreshToken.RevokedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return "Logout successful.Refresh token has been revoked";
        }

        private async Task<string> GenerateJwtToken(User user)
        {

            // Define the signing credentials using the RSA security key and specifying the algorithm
            // var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? "Jf7#9x!pQz@3VeLm4Rs&Tw*8HgBkY1UdN"));
            // var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


            // get dynamic signing credentials
            var signingKey = await _dbContext.SigningKeys.FirstOrDefaultAsync(k => k.IsActive);
            // If no active signing key is found, throw an exception
            if (signingKey == null)
            {
                throw new Exception("No active signing key available.");
            }
            // Convert the Base64-encoded private key string back to a byte array
            var privateKeyBytes = Convert.FromBase64String(signingKey.PrivateKey);
            // Create a new RSA instance for cryptographic operations
            var rsa = RSA.Create();
            // Import the RSA private key into the RSA instance
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
            // Create a new RsaSecurityKey using the RSA instance
            var rsaSecurityKey = new RsaSecurityKey(rsa)
            {
                // Assign the Key ID to link the JWT with the correct public key
                KeyId = signingKey.KeyId
            };
            // Define the signing credentials using the RSA security key and specifying the algorithm
            var creds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            // Iterate through the user's roles and add each as a Role claim
            foreach (var userRole in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Name));
            }
            
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                //signingCredentials: signingCredentials
                signingCredentials: creds
            );
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenDescriptor);
            return token;
        }

        // Helper method to generate a secure random refresh token
        private string GenerateRefreshToken()
        {
            //A secure random string is generated using RandomNumberGenerator
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        // Helper method to hash tokens before storing them
        private string HashToken(string token)
        {
            //The refresh token is hashed using SHA256 before storing it in the database to prevent token theft from compromising security.
            using
            var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(hashedBytes);
        }

    }
}
