using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xgame.Db;

namespace Xgame.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly XgameContext _context;

        public HomeController(XgameContext ctx)
        {
            _context = ctx;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }     

    }
}
