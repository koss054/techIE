namespace techIE.Infrastructure
{
    using Microsoft.AspNetCore.Identity;

    using Data.Entities;

    using static AdminConstants;

    /// <summary>
    /// Extending the ApplicationBuilder in order to add the role Administrator to the seeded Admin user.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Used in Program.cs to make the seeded Admin user an Administrator.
        /// </summary>
        public static IApplicationBuilder SeedAdmin(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var services = scopedServices.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // If the role already exists, it isn't created again.
            // If not, it's created and it's assigned to the seeded Admin - we get this user with his email.
            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdminRoleName };
                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByEmailAsync(AdminEmail);
                await userManager.AddToRoleAsync(admin, role.Name);
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
