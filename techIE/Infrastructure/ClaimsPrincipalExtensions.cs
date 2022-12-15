namespace techIE.Infrastructure
{
    using System.Security.Claims;

    using static AdminConstants;

    /// <summary>
    /// Extension to the claims principal.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get the Id of the user.
        /// </summary>
        /// <param name="user">User with the Id we want to get.</param>
        /// <returns>The Id of the requested user.</returns>
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        /// <summary>
        /// Checks if the user has the admin role.
        /// </summary>
        /// <param name="user">The user we are checking.</param>
        /// <returns>True if the user is an admin.</returns>
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
