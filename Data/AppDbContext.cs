namespace techIE.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Entities;

    using static Constants.DataConstants.User;

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

            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;
    }
}