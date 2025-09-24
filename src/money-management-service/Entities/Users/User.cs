using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace money_management_service.Entities.Users
{
    [Table("USERS")]
    public class User : BaseEntity
    {
        [MaxLength(25)]
        [Required]
        public string UserName { get; set; }

        [MaxLength(25)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100)]
        [MinLength(6)]
        [Required]
        public string Password { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(25)]
        [Required]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
