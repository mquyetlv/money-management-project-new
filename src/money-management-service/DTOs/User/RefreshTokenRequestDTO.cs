using System.ComponentModel.DataAnnotations;

namespace money_management_service.DTOs.User
{
    public class RefreshTokenRequestDTO
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
