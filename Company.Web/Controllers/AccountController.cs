using Company.Data.Models;
using Company.Web.Helpers;
using Company.Web.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> sginInManager)
        {
            _userManager = userManager;
            _sginInManager = sginInManager;
        }

        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _sginInManager { get; }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    if (await _userManager.CheckPasswordAsync(user, input.Password))
                    {
                        var res = await _sginInManager.PasswordSignInAsync(user, input.Password, input.Remember, true);
                        if (res.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = new AppUser()
                {
                    UserName = model.Email.Split("@")[0],
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsActive = true,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _sginInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { Email = input.Email, Token = token }, Request.Scheme);
                    var email = new EmailData
                    {
                        Body = url,
                        Subject = "Reset Your Password",
                        From = "iayman8064@gmail.com",
                        To = input.Email
                    };
                    EmailSettings.SendMail(email);
                    return RedirectToAction(nameof(EmailSent));
                }
            }
            return View();
        }

        public async Task<IActionResult> EmailSent()
        {
            return View();
        }

        public async Task<IActionResult> ResetPassword(string Email, string Token)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, input.Token, input.Password);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));

                }

            }
            return View();
        }

    }
}
