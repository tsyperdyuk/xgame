using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xgame.Core;
using Xgame.Db.Entities;
using Xgame.Model;

namespace Xgame.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionRepository _questionRepository;  
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(IQuestionRepository questionRep, UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr)
        {
            _questionRepository = questionRep;
            _userManager = userMngr;
            _signInManager = signInMngr;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            string userId = HttpContext.User.FindFirst(UserClaimTypes.Id).Value;
            var questions = Mapper.Map<IEnumerable<Question>, List<QuestionRepresentModel>>(await _questionRepository.GetAllQuestionsByUserId(userId));
            ViewBag.UserName = HttpContext.User.Claims.FirstOrDefault().Value;
            ViewBag.Id = userId;
            return View(questions);
        }


    }
}
