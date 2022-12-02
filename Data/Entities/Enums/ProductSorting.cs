namespace techIE.Data.Entities.Enums
{
    /// <summary>
    /// Used for sorting the products in the user marketplace.
    /// The first three values are the official techIE categories.
    /// The last value represents unnofficial categories.
    /// </summary>
    public enum ProductSorting
    {
        Phones = 0,                     // Official
        Laptops = 1,                    // Official
        SmartWatches = 2,               // Official
        Custom = 3
    }
}
