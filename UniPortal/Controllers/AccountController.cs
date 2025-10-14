using Day2MVC.Models;
using Day2MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Day2MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        // Show the login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Handle submitted form
        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginUserViewModel loginUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginUserViewModel.UserName, loginUserViewModel.Password, loginUserViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid UserName or Password");
                // Logic to validate user credentials would go here
                // For now, we will just redirect to the home page
                return RedirectToAction("Index", "Home");
            }
            return View("Login", loginUserViewModel);

        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerUserViewModel.Userame,
                    Address = registerUserViewModel.Address,
                };

                IdentityResult result = await userManager.CreateAsync(user, registerUserViewModel.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Instructor"); // ✅ fixed order
                }

                // show errors if failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("Register", registerUserViewModel);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }


    }
}

