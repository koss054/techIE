﻿namespace techIE.Constants
{
    /// <summary>
    /// Constant strings that are used when we RedirectToAction.
    /// </summary>
    public static class RedirectPaths
    {
        // ---------- Home Controller Redirects ----------
        // Default redirect. Goes to the page with the hero images.
        public const string HomeIndexPage = "Index";
        public const string HomeController = "Home";

        // ---------- User Related Redirects ----------
        // Successfully logged in users are redirected to the Index page of the Home controller.
        // Not using the constants above, in case the user needs to be redirected to a different page.
        public const string UserLoginPage = "Index";
        public const string UserLoginController = "Home";

        // Successfully registered users are redirected to the Login page of the User controller.
        public const string UserRegisterPage = "Login";
        public const string UserRegisterController = "User";

        // ---------- Admin Controller Redirects ----------
        public const string UpdateCategoryPage = "Categories";
        public const string UpdateCategoryController = "Panel";

        public const string UpdateProductPage = "Products";
        public const string UpdateProductController = "Panel";

        public const string NoAvailableCategoriesPage = "Empty";
        public const string NoAvailableCategoriesController = "Panel";

        // ---------- Official Store Controller Redirects ----------
        public const string ProductIsOfficialPage = "Details";
        public const string ProductIsOfficialController = "Product";
        public const string ProductIsOfficialArea = "Official";

        // ---------- Marketplace Controller Redirects ----------
        public const string AddMarketplaceProductPage = "Index";
        public const string AddMarketplaceProductController = "Store";

        public const string NoMarketplaceCategoriesPage = "Empty";
        public const string NoMarketplaceCategoriesController = "Store";

        public const string DeleteMarketplaceProductPage = "Products";
        public const string DeleteMarketplaceProductController = "User";

        public const string ProductIsNotOfficialPage = "Details";
        public const string ProductIsNotOfficialController = "Product";
        public const string ProductIsNotOfficialArea = "Marketplace";

        // ---------- Order Controller Redirects ----------
        public const string AddProductToOrderPage = "Index";
        public const string AddProductToOrderController = "Store";
        public const string FinishOrderPage = "History";
        public const string FinishOrderController = "Order";

        // ---------- Cart Controller Redirects ----------
        public const string CartInspectPage = "Inspect";
        public const string CartInspectController = "Cart";

        // ---------- Product Controller Redirects ----------
        public const string MarketplaceEditPage = "Index";
        public const string MarketplaceEditController = "Store";
    }
}
