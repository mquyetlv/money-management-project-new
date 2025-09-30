using FluentValidation;
using money_management_service.Validations;

namespace money_management_service.Configurations.Validation
{
    public static class ValidatorRegistration
    {
        public static IServiceCollection AddApplicationValidator(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CommandValidation>();
            services.AddValidatorsFromAssemblyContaining<FunctionValidation>();
            services.AddValidatorsFromAssemblyContaining<RoleValidator>();
            services.AddValidatorsFromAssemblyContaining<UserValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateUpdateTransactionTypeValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateUpdateInvestmentValidator>();
            return services;
        }
    }
}
