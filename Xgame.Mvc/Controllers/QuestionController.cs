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
using Xgame.Db.Entities;
using Xgame.Model;
using Xgame.Model.QuestionModel;

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
        public async Task<IActionResult> Edit(int id)
        {
            var x = await _questionRepository.GetById(id).ConfigureAwait(false);
            var question = Mapper.Map<Question, QuestionUpdateModel>(x);
            ViewData["QuestionImage"] = "/Pictures/" + question.QuestionImageUrl;
            ViewData["AnswerImage"] = "/Pictures/" + question.AnswerImageUrl;
            return View(question);
        }

        public IActionResult Delete(int id)
        {
            var question = _questionRepository.GetById(id);
            _questionRepository.Delete(question);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> QuestionList()
        {
            var questions = Mapper.Map<IEnumerable<Question>, List<QuestionRepresentModel>>(await _questionRepository.GetAllQuestions());
            return View(questions);
        }

        [HttpGet]
        public async Task<IActionResult> Review(int id)
        {
            var question = Mapper.Map<Question, QuestionReviewModel>(await _questionRepository.GetById(id));
            ViewData["QuestionImage"] = "/Pictures/" + question.QuestionImageUrl;
            ViewData["AnswerImage"] = "/Pictures/" + question.AnswerImageUrl;
            return View(question);
        }

        [HttpGet]
        public async Task<IActionResult> Reject(int id)
        {
            var question = Mapper.Map<Question, QuestionRejectModel>(await _questionRepository.GetById(id));
            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Reject(QuestionRejectModel questionModel)
        {
            if (ModelState.IsValid)
            {             
                var question = await _questionRepository.GetById(questionModel.Id);
                question.RejectReason = questionModel.RejectReason;
                await _questionRepository.Update(question);
            }
            return  RedirectToAction("QuestionList", "Question");
        }

    }
}
