using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xgame.Model;


namespace Xgame.WebApi.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var temp = new AppUser
            {
                UserName = "Test",
                Email = "Test@gmail.com"
            };
            return View(temp);
        }
    }
}
