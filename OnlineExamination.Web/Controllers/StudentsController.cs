using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using OnlineExamination.BLL.Services;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineExamination.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly IQuestionsService _questionsService;

        public StudentsController(IStudentService studentService, 
            IExamService examService, IQuestionsService questionsService)
        {
            _studentService = studentService;
            _examService = examService;
            _questionsService = questionsService;
        }

        public IActionResult Index(/*int pageNumber=1,int pageSize=10*/)
        {
            return View(_studentService.GetAllStudents());/*(pageNumber,pageSize));*/
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                await _studentService.AddAsync(studentViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }
        public IActionResult AttendExam()
        {
            var model = new AttendExamViewModel();
            LoginViewModel sessionObj = HttpContext.
                Session.Get<LoginViewModel>("loginvm");
            if (sessionObj!=null)
            {
                model.StudentId = Convert.ToInt32(sessionObj.Id);
                model.Questions = new List<QuestionsViewModel>();
                var todayExam = _examService.GetAllExams().Where(a => a.StartDate.Date == DateTime.Today.Date).FirstOrDefault();
                if (todayExam==null)
                {
                    model.Message = "No exam Scheduled today";
                }
                else
                {
                    if (!_questionsService.IsExamAttended(todayExam.Id, model.StudentId))
                    {
                        model.Questions = _questionsService.GetAllQuestionsByExam(todayExam.Id).ToList();
                        model.ExamName = todayExam.Title;
                        model.Message = "";
                    }
                    else
                        model.Message = "You have already attend this exam";
                }
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public IActionResult AttendExam(AttendExamViewModel attendExamViewModel)
        {
            bool result = _studentService.SetExamResult(attendExamViewModel);
            return RedirectToAction("AttendExam");
        }
        public IActionResult Result(String studentsId)
        {
            var model = _studentService.GetExamResults(Convert.ToInt32(studentsId));
            return View(model);
        }
        public IActionResult ViewResult()
        {
            LoginViewModel sessionObj = HttpContext.Session.Get<LoginViewModel>("loginvm");
            if (sessionObj != null)
            {
                var model = _studentService.GetExamResults(Convert.ToInt32(sessionObj.Id));
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
        
        public IActionResult Profile()
        {
            LoginViewModel sessionObj = HttpContext.Session.Get<LoginViewModel>("loginvm");
            if (sessionObj != null)
            {
                var model = _studentService.GetStudentDetails(Convert.ToInt32(sessionObj.Id));
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public IActionResult Profile([FromForm]StudentViewModel studentViewModel)
        {
            _studentService.UpdateAsync(studentViewModel);
            return RedirectToAction("Profile");
        }
    }
}
