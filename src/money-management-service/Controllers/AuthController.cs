using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using money_management_service.Core;
using money_management_service.DTOs.User;
using money_management_service.Services.Interfaces;
using System.Security.Claims;

namespace money_management_service.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthencationService _authService;
        private readonly IValidator<LoginDTO> _validator;
        private readonly IValidator<RefreshTokenRequestDTO> _validatorRequestRefreshTokenDTO;

        public AuthController(IAuthencationService authService, IValidator<LoginDTO> validator, IValidator<RefreshTokenRequestDTO> validatorRequestRefreshTokenDTO)
        {
            _authService = authService;
            _validator = validator;
            _validatorRequestRefreshTokenDTO = validatorRequestRefreshTokenDTO;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<TokenResponseDTO>> Login([FromBody] LoginDTO loginDto)
        {
            var result = _validator.Validate(loginDto);
            if (!result.IsValid)
            {
                List<ErrorDetail> details = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Invalid body", details));
            }

            TokenResponseDTO token = await _authService.Login(loginDto.UserName, loginDto.Password);

            if (token == null)
            {
                return BadRequest("Invalid username or password!");
            }
            return Ok(new { token });
        }


        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO refreshRequestDto)
        {

            var result = _validatorRequestRefreshTokenDTO.Validate(refreshRequestDto);
            if (!result.IsValid)
            {
                List<ErrorDetail> details = result.Errors.Select(err => new ErrorDetail(err.PropertyName, err.ErrorMessage)).ToList();
                return BadRequest(new ErrorResponse("400", "Invalid body", details));
            }

            TokenResponseDTO token = await _authService.RefreshToken(refreshRequestDto);

            if (token == null)
            {
                return BadRequest("Invalid refresh token.");
            }
            return Ok(new { token });
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDTO requestDto)
        {
            string result = await _authService.LogOut(requestDto, User);
            return Ok(new { result });
        }
    }
}
