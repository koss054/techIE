using Microsoft.AspNetCore.Mvc;

namespace techIE.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
