using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Xgame.Core;
using Xgame.Db.Entities;
using Xgame.Model;
using Xgame.Model.QuestionModel;

namespace Xgame.Mvc.Controllers
{
    public class ApproveController : Controller
    {
        private readonly IQuestionRepository _questionRepository;

        public ApproveController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var questions = Mapper.Map<IEnumerable<Question>, List<QuestionRepresentModel>>(_questionRepository.GetAllQuestions());
            return View(questions);
        }

        [HttpGet]
        public IActionResult Review(int id)
        {
            var question = Mapper.Map<Question, QuestionReviewModel>(_questionRepository.GetById(id));
            ViewData["QuestionImage"] = "/Pictures/" + question.QuestionImageUrl;
            ViewData["AnswerImage"] = "/Pictures/" + question.AnswerImageUrl;
            return View(question);
        }
    }
}