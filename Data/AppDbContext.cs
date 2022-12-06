namespace techIE.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using Entities;

    using static Constants.DataConstants.User;
    using static AdminConstants;

    /// <summary>
    /// Context for our database.
    /// Inherits the Identity Context with our expanded User entity.
    /// </summary>
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Overriding the base On Model Creating.
        /// Adding additional requirements/validations for our database entities.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(MaxUserNameLength)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(MaxEmailLength)
                .IsRequired();

            builder.Entity<UserProduct>()
                .HasKey(up => new { up.UserId, up.ProductId});

            SeedAdmin();
            builder.Entity<User>()
                .HasData(AdminUser);

            base.OnModelCreating(builder);
        }

        #region DbSets
        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<UserProduct> UsersProducts { get; set; } = null!;
        #endregion

        #region SeedDatabase
        private void SeedAdmin()
        {
            var hasher = new PasswordHasher<User>();

            AdminUser = new User()
            {
                Id = "sse3f072-d231-e1e1-ab26-1120hhj364e4",
                Email = AdminEmail,
                NormalizedEmail = AdminEmail,
                UserName = AdminUserName,
                NormalizedUserName = AdminUserName,
            };

            AdminUser.PasswordHash =
                hasher.HashPassword(AdminUser, "Admin@dminAdm1n");
        }

        private User AdminUser { get; set; } = null!;
        #endregion
    }
}