namespace techIE.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Data;
    using Data.Entities;
    using Data.Entities.Enums;
    using Models.Carts;
    using Models.Products;

    /// <summary>
    /// Handling all cart logic.
    /// </summary>
    public class CartService : ICartService
    {
        private readonly AppDbContext context;

        public CartService(AppDbContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// Adds product to the cart.
        /// </summary>
        /// <param name="productId">Id of the product that is being added.</param>
        /// <param name="userId">Id of the user who is adding the product to the cart.</param>
        /// <returns>Successful if added. Duplicate if product is already in cart. Failed if product/user doesn't exist.</returns>
        public async Task<CartAction> AddProductAsync(int productId, string userId)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null && product != null)
            {
                return await AddToCurrentAsync(product, user);
            }

            return CartAction.Failed;
        }

        /// <summary>
        /// Checks if the current user has a cart that is active.
        /// </summary>
        /// <param name="userId">Id of user.</param>
        /// <returns>True if user has an active cart. False if user doesn't.</returns>
        public async Task<bool> IsUserCartCurrent(string userId)
        {
            var cart = await context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId &&
                                          c.IsCurrent == true);

            return cart != null ? true : false;
        }

        /// <summary>
        /// Get the current cart of the user.
        /// </summary>
        /// <param name="userId">User whose cart we are trying to get.</param>
        /// <returns>CartViewModel used in the Inspect page, if there is a current cart active.</returns>
        public async Task<CartViewModel?> GetCurrentCart(string userId)
        {
            var cart = await context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId &&
                                          c.IsCurrent == true);

            var cartModel = new CartViewModel();
            if (cart != null)
            {
                cartModel.Id = cart.Id;
                cartModel.UserId = userId;
                cartModel.Total = await GetTotalAsync(cart.Id);
                cartModel.Products = await GetProductsForCurrentAsync(cart.Id);
            }
            return cartModel;
        }

        /// <summary>
        /// Get the total of the cart.
        /// </summary>
        /// <param name="cartId">Id of the cart which total we want to get.</param>
        /// <returns>Total of all products in the requested cart.</returns>
        public async Task<decimal> GetTotalAsync(int cartId)
        {
            decimal total = 0;
            var cartProducts = await context.CartsProducts
                .Where(cp => cp.CartId == cartId)
                .Include(p => p.Product)
                .ToListAsync();

            foreach (var item in cartProducts)
            {
                total += (item.ProductQuantity * item.Product.Price);
            }
            return total;
        }

        /// <summary>
        /// Remove a product from the cart.
        /// If product quantity is higher than 1, the total quantity is reduced by 1 instead of removing the product.
        /// </summary>
        /// <param name="cartId">Id of the cart that has the product being removed.</param>
        /// <param name="productId">Id of the product that is being removed.</param>
        public async Task RemoveProductAsync(int cartId, int productId)
        {
            var cartProduct = await context.CartsProducts
                .FirstOrDefaultAsync(cp => cp.CartId == cartId &&
                                           cp.ProductId == productId);

            if (cartProduct != null)
            {
                if (cartProduct.ProductQuantity > 1)
                {
                    cartProduct.ProductQuantity--;
                }
                else
                {
                    context.CartsProducts.Remove(cartProduct);
                }
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Removes all product from current cart.
        /// </summary>
        /// <param name="cartId">Cart with products to remove.</param>
        public async Task RemoveAllProductsAsync(int cartId)
        {
            var cartProducts = await context.CartsProducts
                .Where(cp => cp.CartId == cartId)
                .ToListAsync();

            if (cartProducts.Count > 0)
            {
                context.CartsProducts.RemoveRange(cartProducts);
                await context.SaveChangesAsync();
            }
        }

        #region PrivateMethods
        /// <summary>
        /// Map product and cart in mapping table CartsProducts.
        /// </summary>
        /// <param name="product">Product that is being added to the cart.</param>
        /// <param name="user">Cart belongs to the passed user.</param>
        /// <returns>Successful if the cart is added. Duplicate if the product is already added to the cart.</returns>
        private async Task<CartAction> AddToCurrentAsync(Product product, User user)
        {
            // Check if the cart already exists.
            var cart = await context.Carts
                .FirstOrDefaultAsync(c => c.UserId == user.Id &&
                                          c.IsCurrent == true);

            // If it doesn't exist, or if it isn't the current one, create a new cart.
            if (cart == null)
            {
                cart = new Cart()
                {
                    IsCurrent = true,
                    UserId = user.Id,
                    User = user
                };
                await context.Carts.AddAsync(cart);
            }

            // If an entry with the same cartId and productId is present in the mapping table, increase product quantity and inform user.
            var duplicate = await context.CartsProducts
                .Where(cp => cp.CartId == cart.Id &&
                             cp.ProductId == product.Id)
                .FirstOrDefaultAsync();

            if (duplicate != null)
            {
                duplicate.ProductQuantity++;
                await context.SaveChangesAsync();
                return CartAction.Duplicate;
            }

            // If there are no duplicate entries, add the product to the cart.
            var cartProduct = new CartProduct()
            {
                CartId = cart.Id,
                Cart = cart,
                ProductQuantity = 1,
                ProductId = product.Id,
                Product = product
            };

            await context.CartsProducts.AddAsync(cartProduct);
            await context.SaveChangesAsync();
            return CartAction.Successful;
        }

        /// <summary>
        /// Gets the products for the current cart.
        /// </summary>
        /// <param name="cartId">Id of the cart that is requesting the products.</param>
        /// <returns>Products that are in the requested cart.</returns>
        private async Task<IEnumerable<ProductCartViewModel>> GetProductsForCurrentAsync(int cartId)
        {
            return await context.CartsProducts
                .Where(cp => cp.CartId == cartId)
                .Include(p => p.Product)
                .Select(p => new ProductCartViewModel()
                {
                    Id = p.Product.Id,
                    Name = p.Product.Name,
                    Price = p.Product.Price,
                    ImageUrl = p.Product.ImageUrl,
                    Quantity = p.ProductQuantity
                }).ToListAsync();
        }
        #endregion
    }
}
