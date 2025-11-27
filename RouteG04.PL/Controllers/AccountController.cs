using Demo.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteG04.DAL.Models.Shared;
using RouteG04.PL.Helper;
using RouteG04.PL.ViewModels.AccountViewModels;
using System.Threading.Tasks;

namespace RouteG04.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerView)
        {
            if(!ModelState.IsValid) return View(registerView);
            var User = new ApplicationUser
            {
                FirstName = registerView.FirstName,
                LastName = registerView.LastName,
                UserName = registerView.UserName,
                Email = registerView.Email

            };
            var result = _userManager.CreateAsync(User, registerView.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerView);
            }
        }
        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            if (!ModelState.IsValid) return View(loginView);
            var User = _userManager.FindByEmailAsync(loginView.Email).Result;
            if(User is not null)
            {
                var result = _signInManager.PasswordSignInAsync(User, loginView.Password, loginView.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt:(");

                }
                
            }
            return View(loginView);
        }
        #endregion
        #region SignOut
        [HttpGet]
        [Authorize]
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
        #region Forget Password
        [HttpGet]
       
        public IActionResult ForgetPassword() => View();
        #endregion
        #region Send Reset Password Link
        [HttpPost]
       
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel forgetPassword)
        {
            if (ModelState.IsValid)
            {
                var User = _userManager.FindByEmailAsync(forgetPassword.Email).Result;
                if (User is not null)
                {
                    //Send Email
                    var Token = _userManager.GeneratePasswordResetTokenAsync(User);
                    var ResetPasswordLink = Url.Action("ResetPassword","Account" ,new {email = forgetPassword.Email,Token},Request.Scheme);
                    var email = new Email()
                    {
                        To = forgetPassword.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink //Link
                    };
                    // Call Send Email => Email
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");


                }
            }
                    ModelState.AddModelError(string.Empty, "Invaild Operation");
                    return View(nameof(ForgetPassword),forgetPassword);
        }
        #endregion
        #region CheckYourInbox
        [HttpGet]
       
        public IActionResult CheckYourInbox() => View();

        #endregion
        #region ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email , string Token)
        {
            TempData["email"] = email;
            TempData["token"] = Token; 
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);
            string email = TempData["email"] as string ?? string.Empty;
            string Token = TempData["Token"] as string ?? string.Empty;
            var User = _userManager.FindByEmailAsync(email).Result;
            if (User is not null)
            {
                var result = _userManager.ResetPasswordAsync(User, Token, resetPassword.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,error.Description);

                    }
                }
            }
            return View(nameof(ResetPassword),resetPassword);
        }
        #endregion
        #region AccessDenied
        public IActionResult AccessDenied() => View();
        #endregion
    }
}
