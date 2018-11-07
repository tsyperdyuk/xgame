using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xgame.Core;
using Xgame.Db;
using Xgame.Db.Entities;
using Xgame.Model;


namespace Xgame.Mvc.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IHostingEnvironment _env;

        public QuestionController(IQuestionRepository questionRep, IHostingEnvironment env)
        {
            _questionRepository = questionRep;
            _env = env;
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
        public IActionResult Create(QuestionCreateModel question, IFormFile picOfQuestion, IFormFile picOfAnswer)
        {
            if (ModelState.IsValid)
            {
                var questionEntity = Mapper.Map<QuestionCreateModel, Question>(question);
                if (picOfQuestion != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picOfQuestion.FileName);
                    var fileNameOfQuestion = Path.Combine(_env.WebRootPath + "\\Pictures", fileName);
                    picOfQuestion.CopyTo(new FileStream(fileNameOfQuestion, FileMode.Create, FileAccess.ReadWrite));
                    questionEntity.QuestionImageUrl = fileName;
                }

                if (picOfAnswer != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(picOfQuestion.FileName);
                    var fileNameOfAnswer = Path.Combine(_env.WebRootPath + "\\Pictures", fileName);
                    picOfAnswer.CopyTo(new FileStream(fileNameOfAnswer, FileMode.Create, FileAccess.ReadWrite));                 
                    questionEntity.AnswerImageUrl = fileName;
                }

                questionEntity.AppUserId = HttpContext.User.FindFirst(UserClaimTypes.Id).Value;               
                _questionRepository.Create(questionEntity);                
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Update(QuestionUpdateModel question)
        {
            if (ModelState.IsValid)
            {
                var questionEntity = Mapper.Map<QuestionUpdateModel, Question>(question);
                questionEntity.AppUserId = HttpContext.User.FindFirst(UserClaimTypes.Id).Value;
                _questionRepository.Update(questionEntity);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Approve(Question questionEntity)
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
            var question = Mapper.Map<QuestionUpdateModel>(_questionRepository.GetById(id)); 
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
