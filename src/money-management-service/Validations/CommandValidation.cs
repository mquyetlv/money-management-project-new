using FluentValidation;
using money_management_service.Dtos.Command;
using System.Net;

namespace money_management_service.Validations
{
    public class CommandValidation : AbstractValidator<CreateUpdateCommandRequestDTO>
    {
        public CommandValidation() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is not empty")
                .Length(3, 50).WithMessage("Name length is between 3 and 50");
        }
    }
}
