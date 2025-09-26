using Microsoft.AspNetCore.Authentication;
using money_management_service.Services;
using money_management_service.Services.Interfaces;

namespace money_management_service.Configurations.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICommandsService, CommandsService>();
            services.AddScoped<IFunctionsService, FunctionsService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAuthencationService, AuthencationService>();
            services.AddScoped<ITransactionTypeService, TransactionTypeService>();

            return services;    
        }
    }
}
