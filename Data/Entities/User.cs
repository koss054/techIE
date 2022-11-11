namespace techIE.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Extension to the ASP.NET Core Identity User.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// If true - the user can create listings in the official store; the user gets a warning when creating a listing in the marketplace.
        /// If false - the user can only create listings in the marketplace (no warning).
        /// </summary>
        [Required]
        public bool IsAdmin { get; set; } = false;

        /// <summary>
        /// Mapping table for the User and the Marketplace Product.
        /// </summary>
        public virtual ICollection<UserProduct> UsersMProducts { get; set; } = new List<UserProduct>();
    }
}
