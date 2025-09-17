using Microsoft.EntityFrameworkCore;
using money_management_service.Entities;
using money_management_service.Entities.Users;

namespace money_management_service.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var baseType = typeof(BaseEntity);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (baseType.IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("Id")
                        .HasDefaultValueSql("NEWID()");
                }
            }

            //modelBuilder.Entity<FunctionCommand>()
            //    .HasKey((entity) => new { entity.FunctionId, entity.CommandId });

            //modelBuilder.Entity<Permission>()
            //    .HasKey(entity => new { entity.RoleId, entity.FunctionId });

            //modelBuilder.Entity<UserRole>()
                //.HasKey(entity => new { entity.RoleId, entity.UserId });
        }

        public DbSet<Command> Commands { get; set; }

        public DbSet<Function> Functions { get; set; }

        //public DbSet<FunctionCommand> FunctionCommand { get; set; }

        //public DbSet<Permission> Permissions { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        //public DbSet<UserRole> UserRole { get; set; }
    }
}
