using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nest_BackFront.Models;
using Nest_BackFront.ViewModels.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nest_BackFront.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }
        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInVM signIn)
        {
            AppUser user;
            if (signIn.UsernamOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(signIn.UsernamOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(signIn.UsernamOrEmail);
            }
            if (user==null)
            {
                ModelState.AddModelError("", "Login veya prol sehfii");
                return View(signIn);
            }
          var  result=await  _signInManager.PasswordSignInAsync(user, signIn.Password, signIn.RememberMe,true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Hedsiz ugursuz ceht.Zehmet olsada olmasada gozleyin");
                return View(signIn);
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Login veya prol sehfii");
                return View(signIn);
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser newUser = new AppUser
            {
                Name = registerVM.Firstname,
                Surname = registerVM.Lastname,
                UserName = registerVM.Username,
                Email = registerVM.Email
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            await _signInManager.SignInAsync(newUser, true);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Register));
        }
    }
}
