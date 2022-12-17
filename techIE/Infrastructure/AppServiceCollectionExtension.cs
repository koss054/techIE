namespace Microsoft.Extensions.DependencyInjection
{
    using techIE.Contracts;
    using techIE.Services;

    /// <summary>
    /// Extend the ServiceCollection.
    /// </summary>
    public static class AppServiceCollectionExtension
    {
        /// <summary>
        /// Add our services to the application.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>Services that our project uses.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
