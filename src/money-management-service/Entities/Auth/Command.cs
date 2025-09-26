using money_management_service.Entities.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace money_management_service.Entities.Users
{
    [Table("COMMANDS")]
    public class Command : BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<RoleFunctionCommand> RoleFunctionCommands { get; set; }
    }
}
