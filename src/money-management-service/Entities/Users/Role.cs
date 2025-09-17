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

        public ICollection<User> Users { get; set; }

        //public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<Function> Functions { get; set; }

        //public ICollection<Permission> Permissions { get; set; }
    }
}
