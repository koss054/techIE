namespace techIE.Models
{
    /// <summary>
    /// View model for our extended user.
    /// Takes only the necessary data from the databaase.
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// ID of the user.
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// The user's username.
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// The user's email.
        /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>
        /// If IsAdmin is true, then the user has access to additional options for the official store and the user marketplace.
        /// If IsAdmin is false, then the user has regular access to the site.
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
