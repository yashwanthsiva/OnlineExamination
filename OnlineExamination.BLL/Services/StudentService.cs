using Microsoft.Extensions.Logging;
using OnlineExamination.DataAccess;
using OnlineExamination.DataAccess.UnitOfWork;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.BLL.Services
{
    public class StudentService:IStudentService
    {
        IUnitOfWork _unitOfWork;
        ILogger<QuestionsService> _iLogger;

        public async Task<StudentViewModel> AddAsync(StudentViewModel vm)
        {
            try
            {
                Students obj = vm.ConvertViewModel(vm);
                await _unitOfWork.GenericRepository<Students>().AddAsync(obj);
            }
            catch(Exception ex)
            {
                return null;
            }
            return vm;
        }

        public PagedResult<StudentViewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new StudentViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<StudentViewModel> detailList = new List<StudentViewModel>();
                var modelList = _unitOfWork.GenericRepository<Students>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<Students>().GetAll().ToList();
                detailList = GroupListInfo(modelList);
                if(detailList!=null)
                {
                    model.StudentList = detailList;
                    model.TotalCount = totalCount.Count();
                }
            }
            catch(Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            var result = new PagedResult<StudentViewModel>
            { 
                Data= model.StudentList,
                TotalItems=model.TotalCount,
                PageNumber=pageNumber,
                PageSize=pageSize
            };
            return result;
        }

        private List<StudentViewModel> GroupListInfo(List<Students> modelList)
        {
            return modelList.Select(o => new StudentViewModel(o)).ToList();
        }

        public IEnumerable<Students> GetAllStudents()
        {
            try
            {
                var students = _unitOfWork.GenericRepository<Students>().GetAll();
                return students;
            }
            catch(Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return Enumerable.Empty<Students>();
        }

        public IEnumerable<ResultViewModel> GetExamResults(int studentId)
        {
            try
            {
                var examResults = _unitOfWork.GenericRepository<ExamResults>().GetAll()
                    .Where(a => a.StudentsId == studentId);
                var students = _unitOfWork.GenericRepository<Students>().GetAll();
                var exams = _unitOfWork.GenericRepository<ExamResults>().GetAll();
                var questions = _unitOfWork.GenericRepository<Questions>().GetAll();

                var requiredData = examResults.Join(students, er => er.StudentsId, s => s.Id,
                    (er, st) => new { er, st }).Join(exams, erj => erj.er.ExamsId, ex => ex.Id,
                    (erj, ex) => new { erj, ex }).Join(questions, exj => exj.erj.er.QuestionsId, q => q.Id,
                    (exj, q) => new ResultViewModel()
                    {
                        StudentId = studentId,
                        ExamName = exj.ex.Title,
                        TotalQuestion =examResults.Count(a=>a.StudentsId==studentId
                         && a.ExamsId==exj.ex.Id), CorrectAnswer = examResults.Count(a=>a.StudentsId==studentId &&
                         a.ExamsId==exj.ex.Id && a.Answer==q.Answer),
                        WrongAnswer = examResults.Count(a=>a.StudentsId==studentId &&
                        a.ExamsId == exj.ex.Id && a.Answer !=q.Answer)
                    });
                return requiredData;

            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }

            return Enumerable.Empty<ResultViewModel>();
        }

        public bool SetExamResult(AttendExamViewModel vm)
        {
            try
            {
                foreach (var item in vm.Questions)
                {
                    ExamResults examResults = new ExamResults();
                    examResults.StudentsId = vm.StudentId;
                    examResults.Questions = item.QuestionsId;
                    examResults.ExamsId = item.ExamsId;
                    examResults.Answer = item.SelectedAnswer;
                    _unitOfWork.GenericRepository<ExamResults>().AddAsync(examResults);
                }
                _unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return false;
        }

        public bool SetGroupIdToStudents(GroupViewModel vm)
        {
            try
            {
            foreach (var item in vm.GroupList)
            {
                    var student = _unitOfWork.GenericRepository<Students>().GetByID(item.Id);
                    if (item.Selected)
                    {
                        student.GroupsId = vm.Id;
                        _unitOfWork.GenericRepository<Students>().Update(student);
                    }
                    else
                    {
                        if (student.GroupsId == vm.Id)
                        {
                            student.GroupsId = null;
                        }
                    }
                    _unitOfWork.Save();
                    return true;
            }
        }
            catch(Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return false;
        }

        public async Task<StudentViewModel> UpdateAsync(StudentViewModel vm)
        {
            try
            {
                Students obj = _unitOfWork.GenericRepository<Students>().GetByID(vm.Id);
                obj.Name = vm.Name;
                obj.Username = vm.UserName;
                obj.Contact = vm.Contact;
                await _unitOfWork.GenericRepository<Students>().UpdateAsync(obj);
                _unitOfWork.Save();
            }
            catch(Exception)
            {
                throw;
            }
            return vm;
        }

        public StudentViewModel GetStudentDetails(int studentId)
        {
            try
            {
                var student = _unitOfWork.GenericRepository<Students>().GetByID(studentId);
                return student != null ? new StudentViewModel(student) :  null;
            }
            catch(Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return null;
        }
    }
}
