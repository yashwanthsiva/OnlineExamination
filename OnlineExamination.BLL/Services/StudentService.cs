using OnlineExamination.DataAccess;
using OnlineExamination.DataAccess.UnitOfWork;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public bool SetExamResult(AttendExamViewModel vm)
        {
            throw new NotImplementedException();
        }

        public bool SetGroupIdToStudents(GroupViewModel vm)
        {
            throw new NotImplementedException();
        }

        public Task<StudentViewModel> UpdateAsync(StudentViewModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
