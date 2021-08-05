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

        public Task<StudentViewModel> AddAsync(StudentViewModel vm)
        {
            throw new NotImplementedException();
        }

        public PagedResult<StudentViewModel> GetAll(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Students> GetAllStudents()
        {
            throw new NotImplementedException();
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
                    (er, st) => new { er, st }).Join(exams, erj => erj.er.ExamsID, ex => ex.Id,
                    (erj, ex) => new { erj, ex }).Join(questions, exj => exj.erj.er.QuestionsID, q => q.Id,
                    (exj, q) => new ResultViewModel()
                    {
                        StudentId = studentId,
                        ExamName = exj.ex.Title,
                        TotalQuestion =examResults.Count(a=>a.StudentsId==studentId
                         && a.ExamsId==exj.ex.Id), CorrectAnswer = examResults.Count(a=>a.StudentsId==studentId &&
                         a.ExamsId==exj.ex.Id && a.Answer==q.Answer),
                        WrongAnswer = examResults.Count(a=>StudentsId==studentId &&
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
                    examResults.ExamsID = item.ExamsId;
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

        public Task<StudentViewModel> UpdateAsync(StudentViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
