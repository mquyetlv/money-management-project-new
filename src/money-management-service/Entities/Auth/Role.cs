using money_management_service.Entities.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace money_management_service.Entities.Users
{
    [Table("ROLES")]
    public class Role : BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<RoleFunctionCommand> RoleFunctionCommands { get; set; }
    }
}
