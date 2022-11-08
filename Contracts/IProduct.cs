namespace techIE.Contracts
{
    using Data.Entities;
    using Data.Entities.Enums;

    /// <summary>
    /// An interface which is used for both product entities.
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Name of the product.
        /// Always visualized when the product is on a page.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Image of the product.
        /// Always visualized when the product is on a page.
        /// Using urls/image path. Less memory in database.
        /// techIE owns imgur and wants the users the use their image hosting website to keep the product images ;)
        /// </summary>
        string Image { get; }

        /// <summary>
        /// Price of the product.
        /// Always visualized when the product is on a page.
        /// </summary>
        decimal Price { get; }

        /// <summary>
        /// Color of the product.
        /// Changes the image on the screen, depending on the chosen color.
        /// Colorful is used when the provided item has a different color than the available ones.
        /// </summary>
        Color Color { get; }

        /// <summary>
        /// Description of the product.
        /// Only visualized when the user opens the page of the product.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The value is true when the product is listed by techIE.
        /// The value is false when the product is listed by a user.
        /// </summary>
        bool IsOfficial { get; }

        /// <summary>
        /// Category of the product.
        /// Depending on it the product is visualized on a page or not.
        /// Three official store categories.
        /// </summary>
        Category Category { get; }
    }
}
