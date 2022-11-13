namespace techIE.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Models.Products;

    /// <summary>
    /// Controller that handles the logic for techIE's official store.
    /// Only items with IsOfficial == true will be displayed here.
    /// </summary>
    public class OfficialStoreController : BaseController
    {
        public IActionResult Add()
        {
            var model = new AddProductViewModel();
           
        }
    }
}
