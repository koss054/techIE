namespace techIE.Areas.Marketplace.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using techIE.Controllers;

    /// <summary>
    /// Store controller for the user marketplace.
    /// Allows the users to browse the listings of others and create their own.
    /// </summary>
    public class StoreController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
