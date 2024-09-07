using Bloggie.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Bloggie.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public SignInManager<IdentityUser> SignInManager { get; }

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            SignInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
            };

            var result = await userManager.CreateAsync(identityUser, registerViewModel.Password);
            if (result.Succeeded)
            {
                // assign the role 
                var roleResult = await userManager.AddToRoleAsync(identityUser, "User");
                if (roleResult.Succeeded)
                {
                    // show success
                    return RedirectToAction("Register");
                }
            }
            // show failure
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };
            return View(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var signInResult = await SignInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (signInResult.Succeeded && signInResult != null)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl)) {

                    return Redirect(loginViewModel.ReturnUrl);
                }
                // show success
                return RedirectToAction("Index", "Home");
            }
            // show failure
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            //await SignInManager.SignOutAsync();
            //return RedirectToAction("Index", "Home");
            return View();
        }

    }
}
