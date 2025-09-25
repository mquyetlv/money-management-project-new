using Microsoft.AspNetCore.Mvc;
using money_management_service.DTOs.User;
using System.Security.Claims;

namespace money_management_service.Services.Interfaces
{
    public interface IAuthencationService
    {
        public Task<TokenResponseDTO> Login(string username, string password);

        public Task<TokenResponseDTO> RefreshToken(RefreshTokenRequestDTO requestDto);

        public Task<string> LogOut(RefreshTokenRequestDTO requestDto, ClaimsPrincipal userClaims);
    }
}
