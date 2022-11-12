namespace techIE.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Constants.DataConstants.User;

    /// <summary>
    /// View model used when the user tries to register.
    /// Used for User controller Register().
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Username of the user when registering.
        /// </summary>
        [Required]
        [Display(Name = DisplayUserName)]
        [StringLength(MaxUserNameLength, MinimumLength = MinUserNameLength)]
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Email of the user when registering.
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(MaxEmailLength, MinimumLength = MinEmailLength)]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Confirmation of the email when the user is registering.
        /// </summary>
        [EmailAddress]
        [Compare(nameof(Email))]
        public string ConfirmEmail { get; set; } = null!;

        /// <summary>
        /// Password of the user when registering.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(MaxPasswordLength, MinimumLength = MinPasswordLength)]
        public string Password { get; set; } = null!;

        /// <summary>
        /// Confirmation of password when the user is registering.
        /// </summary>
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
