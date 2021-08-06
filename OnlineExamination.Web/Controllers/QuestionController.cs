using Microsoft.AspNetCore.Mvc;
using OnlineExamination.BLL.Services;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExamination.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IExamService _examService;
        private readonly IQuestionsService _questionService;

        public QuestionController(IExamService examService, IQuestionsService questionService)
        {
            _examService = examService;
            _questionService = questionService;
        }

        public IActionResult Index(int pageNUmber=1, int pageSize=10)
        {
            return View(_questionService.GetAll(pageNUmber, pageSize));
        }
        public IActionResult Create()
        {
            var model = new QuestionsViewModel();
            model.ExamList = _examService.GetAllExams();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(QuestionsViewModel questionsViewModel)
        {
            if(ModelState.IsValid)
            {
                await _questionService.AddAsync(questionsViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(questionsViewModel);
        }

    }
}
