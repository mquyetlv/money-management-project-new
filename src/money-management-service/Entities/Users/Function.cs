using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace money_management_service.Entities.Users
{
    [Table("FUNCTIONS")]
    public class Function : BaseEntity
    {
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }

        [MaxLength(255)]
        [Required]
        public string Url { get; set; }

        public ICollection<Command> Commands { get; set; }

        //public ICollection<FunctionCommand> FunctionCommands { get; set; }

        public ICollection<Role> roles { get; set; }

        //public ICollection<Permission> Permissions { get; set; }
    }
}
