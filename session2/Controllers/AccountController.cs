using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using session2.Models;

namespace session2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(SignInManager<AppUser> _signInManager, 
            UserManager<AppUser> _userManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(
                    model.Username!, model.Password!, model.RememberMe,false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError("", "Invalid Login attempt");
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Name = model.Name!,
                    UserName = model.Email!,
                    Email = model.Email,
                    Address = model.Address!
                };
                var result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                foreach (var error in result.Errors)
                    ModelState.AddModelError("",error.Description);
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
