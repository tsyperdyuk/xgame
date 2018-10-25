using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Xgame.Core;
using Xgame.Db;
using Xgame.Db.Entities;
using Xgame.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Xgame.Mvc.Controllers
{
    public class QuestionController : Controller
    {
        private readonly XgameContext _context;

        public QuestionController(XgameContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
                
        [HttpPost]
        public async Task<IActionResult> Create(QuestionCreateModel question)
        {
            if (ModelState.IsValid)
            {
                var questionEntity = Mapper.Map<Question>(question);
                questionEntity.AppUserId = HttpContext.User.FindFirst(UserClaimTypes.Id).ToString();
                _context.Questions.Add(questionEntity);
                await _context.SaveChangesAsync();                
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
