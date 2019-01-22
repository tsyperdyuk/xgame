using System;
using System.Collections.Generic;
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

                questionEntity.QuestionImageUrl = picOfQuestion.SavePictureFileUpload(_env);
                questionEntity.AnswerImageUrl = picOfAnswer.SavePictureFileUpload(_env);

                questionEntity.CreatedDate = DateTime.Now;
                questionEntity.Status = Status.New.ToString();
                questionEntity.AppUserId = HttpContext.User.FindFirst(UserClaimTypes.UserId).Value;

                _questionRepository.Create(questionEntity);                
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Update(QuestionUpdateModel question, IFormFile picOfQuestion, IFormFile picOfAnswer)
        {
            if (ModelState.IsValid)
            {
                var questionEntity = Mapper.Map<QuestionUpdateModel, Question>(question);
                questionEntity.AppUserId = HttpContext.User.FindFirst(UserClaimTypes.UserId).Value;

                questionEntity.QuestionImageUrl = picOfQuestion.SavePictureFileUpload(_env);
                questionEntity.AnswerImageUrl = picOfAnswer.SavePictureFileUpload(_env);

                _questionRepository.Update(questionEntity);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Approve(int questionId)
        {
            var question = await _questionRepository.GetById(questionId).ConfigureAwait(false);
            question.UpdatedDate = DateTime.Now;
            question.Status = Status.Approved.ToString();
            await _questionRepository.Update(question);
            
            return RedirectToAction("QuestionList", "Question");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var questionEntity = await _questionRepository.GetById(id).ConfigureAwait(false);
            var question = Mapper.Map<Question, QuestionUpdateModel>(questionEntity);
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
        public async Task<IActionResult> QuestionList2(int pageIndex = 1, int pageSize = 10)
        {
            var pagedQuestion = await _questionRepository.GetQuestions(pageIndex, pageSize, null);
            var questions = Mapper.Map<IEnumerable<Question>, List<QuestionRepresentModel>>(pagedQuestion.List);
            return View(questions);
        }

        [HttpGet]
        public async Task<JsonResult> GetQuestionList(int pageIndex = 1, int pageSize = 10,
            string draw = null, string start = null)
        {
            var res = new
            {
                draw = 1,
                recordsTotal = 15,
                recordsFiltered = 5,
                data = new [] {
                    new [] { "Test", "Field2" },
                    new [] { "Test2", "Field3" },
                    new [] { "Test3", "Field4" },
                    new [] { "Test4", "Field5" },
                    new [] { "Test5", "Field6" },
                    new [] { "Test", "Field2" },
                    new [] { "Test2", "Field3" },
                    new [] { "Test3", "Field4" },
                    new [] { "Test4", "Field5" },
                    new [] { "Test5", "Field6" },
                    new [] { "Test", "Field2" },
                    new [] { "Test2", "Field3" },
                    new [] { "Test3", "Field4" },
                    new [] { "Test4", "Field5" },
                    new [] { "Test5", "Field6" },
                }
            };

            await Task.CompletedTask;

            return Json(res);
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
        public async Task<IActionResult> Reject(int questionId)
        {
            var question = Mapper.Map<Question, QuestionRejectModel>(await _questionRepository.GetById(questionId));
            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Reject([FromForm]QuestionRejectModel model)
        {
            var question = await _questionRepository.GetById(model.Id);
            question.RejectReason = model.RejectReason;
            question.Status = Status.Rejected.ToString();
            question.UpdatedDate = DateTime.Now;
            await _questionRepository.Update(question);
            return  RedirectToAction("QuestionList", "Question");
        }

    }
}
