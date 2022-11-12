namespace techIE.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;

    using Models;
    using Constants;
    using Data.Entities;

    /// <summary>
    /// Controller used for user operations - login, register, logout.
    /// </summary>
    public class UserController : BaseController
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public UserController(
            SignInManager<User> _signInManager,
            UserManager<User> _userManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(
                RedirectPaths.HomeIndexPage,
                RedirectPaths.HomeController);
        }

        /// <summary>
        /// If the user is already logged in they get redirected to the home page.
        /// Otherwise, the user is redirected to the login page, where they need to enter their account details.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(
                    RedirectPaths.HomeIndexPage,
                    RedirectPaths.HomeController);
            }

            var model = new LoginViewModel();
            return View(model);
        }

        /// <summary>
        /// If the details aren't correct or if the model state isn't valid, the page informs the user and they can try again.
        /// If the login is successful, the user is redirected to the home page.
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction(
                        RedirectPaths.UserLoginPage,
                        RedirectPaths.UserLoginController);
                }
            }

            ModelState.AddModelError(
                string.Empty,
                ErrorMessages.InvalidUserDetails);
            return View(model);
        }

        /// <summary>
        /// If the user is already logged in, they are redirected to the home page.
        /// Otherwise, the user is prompted to enter their account details.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(
                    RedirectPaths.HomeIndexPage,
                    RedirectPaths.HomeController);
            }

            var model = new RegisterViewModel();
            return View(model);
        }

        /// <summary>
        /// If the details aren't correct or if the model state isn't valid, the page informs the user and they can try again.
        /// If the registration is successful, the user is redirected to the login page.
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(
                    RedirectPaths.UserRegisterPage,
                    RedirectPaths.UserRegisterController);
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }

            return View(model);
        }
    }
}
