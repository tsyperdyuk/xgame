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
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRep)
        {
            _questionRepository = questionRep;
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
        public IActionResult Create(QuestionCreateModel question)
        {
            if (ModelState.IsValid)
            {
                var questionEntity = Mapper.Map<Question>(question);
                questionEntity.AppUserId = HttpContext.User.FindFirst(UserClaimTypes.Id).Value;
                _questionRepository.Create(questionEntity);                
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Update(Question questionEntity)
        {
            if (ModelState.IsValid)
            {
                questionEntity.AppUserId = HttpContext.User.FindFirst(UserClaimTypes.Id).Value;
                _questionRepository.Update(questionEntity);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {            
            var question = _questionRepository.GetById(id);
            return View(question);
        }

        public IActionResult Delete(int id)
        {
            var question = _questionRepository.GetById(id);

            _questionRepository.Delete(question);

            return RedirectToAction("Index", "Home");
        }

    }
}
