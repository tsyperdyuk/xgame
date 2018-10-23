using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xgame.Db;
using Xgame.Db.Entities;

namespace Xgame.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly XgameContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(XgameContext ctx, UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr)
        {
            _context = ctx;
            _userManager = userMngr;
           _signInManager = signInMngr;
    }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.User.Claims.FirstOrDefault().Value;
            return View(User.Identity.IsAuthenticated);
        }     

    }
}
