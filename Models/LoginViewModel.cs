namespace techIE.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Constants.DataConstants.User;

    /// <summary>
    /// View model used when the user is trying to login.
    /// Used for User controller Login().
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Username entered by the user, trying to login.
        /// </summary>
        [Required]
        [Display(Name = DisplayUserName)]
        [StringLength(MaxUserNameLength, MinimumLength = MinUserNameLength)]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Password entered by the user, trying to login.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(MaxPasswordLength, MinimumLength = MinPasswordLength)]
        public string Password { get; set; } = null!;
    }
}
