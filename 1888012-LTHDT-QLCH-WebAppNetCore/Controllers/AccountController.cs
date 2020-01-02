using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1888012_LTHDT_QLCH_WebAppNetCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1888012_LTHDT_QLCH_WebAppNetCore.Controllers
{
    //Only this controller can be accessed before login
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        //UserManager to CRUD users, SignInManager to SignIn and SignOut
        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,false);
                if (result.Succeeded)
                {
                    //returnUrl parameter is automatically mapped by the framework and pass to our action
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl) )
                    {
                        //Use LocalRedirect instead of redirect to avoid open redirect attack
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty,"Invalid login attempt");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //This method works with js validation and Remote attribute to make a remote validation for email duplication check for Register Action
        [AcceptVerbs("Get","Post")]
        public async Task<IActionResult> IsEmailValid(string email)
        {
            string[] strings = email.Split('@');
            
            if (strings[1].ToUpper() == "GMAIL.COM")
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return Json(true);
                }
                else
                {
                    return Json($"Email {email} is already in use!");
                }
            }
            else
            {
                return Json("Domain name must be google.com");
            }
       
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        //Account controller has userManager and signInManager, they will work with roleManager to control user access ability
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}