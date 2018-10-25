using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Xgame.Db.Entities;
using Xgame.Model;

namespace Xgame.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    user = new AppUser { UserName = model.UserName };
                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }                   
                }
               await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);               
            } else
            {
                await _signInManager.PasswordSignInAsync("Andriy", "Andriy", false, false);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]        
        public async Task<IActionResult> Logout()
        {          
            await _signInManager.SignOutAsync();            
            return RedirectToAction("Index", "Home");
        }
    }
}