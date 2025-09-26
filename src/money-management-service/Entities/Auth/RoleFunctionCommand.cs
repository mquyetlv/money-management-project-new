using money_management_service.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace money_management_service.Entities.Auth
{
    [Table("ROLE_FUNCTION_COMMAND")]
    public class RoleFunctionCommand
    {
        public Guid RoleId { get; set; }

        public Guid FunctionId { get; set; }

        public Guid CommandId { get; set; }

        public Role Role { get; set; }

        public Function Function { get; set; }

        public Command Command { get; set; }
    }
}
