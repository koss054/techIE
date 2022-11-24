namespace techIE.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// Home controller.
    /// The index page is here.
    /// Error pages and additional info about this company is also here.
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// Index page of techIE.
        /// It has the hero image and access to all categories under it.
        /// </summary>
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Privacy policy.
        /// Still empty. May abandon completely.
        /// </summary>
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Error pages.
        /// Depending on the status code of the error the appropriate error page is displayed.
        /// </summary>
        /// <param name="statusCode">Status code of the error.</param>
        [AllowAnonymous]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error400");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}