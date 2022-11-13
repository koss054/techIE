namespace techIE.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contracts;
    using Data.Entities;

    /// <summary>
    /// Handling all product logic.
    /// </summary>
    public class ProductService : IProductService
    {
        public Task<IEnumerable<Category>> GetOfficialCategoriesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
