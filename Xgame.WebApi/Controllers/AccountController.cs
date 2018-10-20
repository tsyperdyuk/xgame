using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Xgame.Model;


namespace Xgame.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManeger;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr)
        {
            userManeger = userMngr;
            signInManager = signInMngr;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManeger.FindByEmailAsync(details.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded)
                    {

                        return RedirectToAction("Index", "Home");
                        //return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.UserName), "Invalid user or password");
            }
            return View(details);
        }
    }
}

