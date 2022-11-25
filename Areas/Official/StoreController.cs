namespace techIE.Areas.Official.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using techIE.Controllers;
    using Models.Products;

    /// <summary>
    /// Controller that handles the logic for techIE's official store.
    /// Only items with IsOfficial == true will be displayed here.
    /// </summary>
    public class StoreController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
