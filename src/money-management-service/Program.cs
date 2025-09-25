using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using money_management_service.Data;
using money_management_service.Jobs;
using money_management_service.Middlewares;
using money_management_service.Services;
using money_management_service.Services.Interfaces;
using money_management_service.Validations;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));

// Register Background service
builder.Services.AddHostedService<KeyRotationService>();

// Configure Authentication using JWT Bearer tokens
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        // Define token validation parameters to ensure tokens are valid and trustworthy
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Ensure the token was issued by a trusted issuer
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // The expected issuer value from configuration
            ValidateAudience = false, // Disable audience validation (can be enabled as needed)
            ValidateLifetime = true, // Ensure the token has not expired
            ValidateIssuerSigningKey = true,
                                             
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                var httpClient = new HttpClient();
                var jwks = httpClient.GetStringAsync($"{builder.Configuration["Jwt:Issuer"]}/.well-known/jwks.json").Result;
                var keys = new JsonWebKeySet(jwks);
                return keys.Keys;
            }
        };
    });


// Đăng ký fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<CommandValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<FunctionValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<RoleValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

// Register Service
builder.Services.AddScoped<ICommandsService, CommandsService>();
builder.Services.AddScoped<IFunctionsService, FunctionsService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAuthencationService, AuthencationService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
