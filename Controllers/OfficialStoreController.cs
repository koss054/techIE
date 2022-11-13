using Microsoft.AspNetCore.Mvc;

namespace techIE.Controllers
{
    /// <summary>
    /// Controller that handles the logic for techIE's official store.
    /// Only items with IsOfficial == true will be displayed here.
    /// </summary>
    public class OfficialStoreController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
