using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xgame.Core;

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
            var myQuestions = _questionRepository.GetAllQuestions();        
            return View(myQuestions);
        }

        [HttpGet]
        public IActionResult Review(int id)
        {
            var question = _questionRepository.GetById(id);
            return View(question);
        }
    }
}