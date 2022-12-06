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
        /// Mapping table for the User and the Marketplace Product.
        /// </summary>
        public virtual ICollection<UserProduct> UsersProducts { get; set; } = new List<UserProduct>();
    }
}
