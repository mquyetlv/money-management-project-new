using Microsoft.EntityFrameworkCore;
using money_management_service.Data;
using money_management_service.Services;
using money_management_service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString));

// Register Dependency injection
builder.Services.AddScoped<ICommandService, CommandService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
