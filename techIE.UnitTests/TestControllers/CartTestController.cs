namespace techIE.UnitTests.TestControllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using techIE.Controllers;
    using techIE.Constants;
    using techIE.Contracts;
    using techIE.Infrastructure;
    using techIE.Data.Entities.Enums;

    /// <summary>
    /// Controller used for managing carts.
    /// Used in both official and marketplace areas.
    /// </summary>
    public class CartTestController : BaseController
    {
        private readonly ICartService cartService;
        private string testUserId = "fake-guid"; // Specifying this here, because I can't test the static ClaimsPrincipalExtensions User.Id().

        public CartTestController(
            ICartService _cartService)
        {
            cartService = _cartService;
        }

        /// <summary>
        /// Add product to current cart.
        /// </summary>
        /// <param name="id">Id of product that is being added.</param>
        /// <param name="store">Store can be Official or Marketplace, depending on where the product is being added from.</param>
        /// <returns>Depending on the enum CartAction, the user is either redirected to an appropriate page or gets BadRequest.</returns>
        public async Task<IActionResult> Add(int id, string store)
        {
            var action = await cartService.AddProductAsync(id, testUserId);

            if (action == CartAction.Failed)
            {
                return BadRequest();
            }

            if (action == CartAction.Successful)
            {
                TempData["Cart"] = Messages.CartActionSuccessful;
            }
            else if (action == CartAction.Duplicate)
            {
                TempData["Cart"] = Messages.CartActionDuplicate;
            }

            return RedirectToAction(
                    RedirectPaths.AddProductToOrderPage,
                    RedirectPaths.AddProductToOrderController,
                    new { area = store });
        }

        /// <summary>
        /// Inspects the cart. 
        /// The user can view all currently added products, increase their quantity or remove them.
        /// </summary>
        /// <returns>Cart content if there are items in the cart. Otherwise, the user is informed that their cart is empty..</returns>
        public async Task<IActionResult> Inspect()
        {
            var model = await cartService.GetCurrentCartAsync(testUserId);
            return View(model);
        }

        /// <summary>
        /// Removes a product from the cart.
        /// If product quanity is more than 1, it only reduces the total quantity.
        /// </summary>
        /// <param name="cartId">Id of cart from which we are removing a product.</param>
        /// <param name="productId">Product which we are removing/reducing.</param>
        /// <returns>Redirects to the cart inspect page.</returns>
        public async Task<IActionResult> Remove(int cartId, int productId)
        {
            if (await cartService.IsCartForUserAsync(cartId, testUserId) == false)
            {
                return NotFound();
            }

            await cartService.RemoveProductAsync(cartId, productId);
            return RedirectToAction(
                RedirectPaths.CartInspectPage,
                RedirectPaths.CartInspectController);
        }

        /// <summary>
        /// Empties out the cart. No products are left, despite their quantity.
        /// </summary>
        /// <param name="cartId">Id of cart which we are emptying out.</param>
        /// <returns>Redirects to the cart inspect page.</returns>
        public async Task<IActionResult> Empty(int cartId)
        {
            if (await cartService.IsCartForUserAsync(cartId, testUserId) == false)
            {
                return NotFound();
            }

            await cartService.RemoveAllProductsAsync(cartId);
            return RedirectToAction(
                RedirectPaths.CartInspectPage,
                RedirectPaths.CartInspectController);
        }

        public async Task<IActionResult> History(int cartId) 
        {
            if (await cartService.IsCartForUserAsync(cartId, testUserId) == false)
            {
                return NotFound();
            }

            var model = await cartService.GetCartAsync(cartId);
            return View(model);
        }
    }
}
