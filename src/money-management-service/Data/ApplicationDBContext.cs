using Microsoft.EntityFrameworkCore;
using money_management_service.Entities;
using money_management_service.Entities.Auth;
using money_management_service.Entities.Transaction;
using money_management_service.Entities.Users;

namespace money_management_service.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleFunctionCommand>().HasKey(entity => new { entity.RoleId, entity.FunctionId, entity.CommandId });

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
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Command> Commands { get; set; }

        public DbSet<Function> Functions { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SigningKey> SigningKeys { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<RoleFunctionCommand> RoleFunctionCommands { get; set; }

        public DbSet<TransactionType> TransactionTypes { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Investment> Investments { get; set; }

        public DbSet<Accounts> Accounts { get; set; }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var currentUsername = "quyetlv";

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = currentUsername;
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = currentUsername;
                }

                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = currentUsername;
                }
            }
        }
    }
}
