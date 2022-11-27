namespace techIE.Constants
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
    }
}
