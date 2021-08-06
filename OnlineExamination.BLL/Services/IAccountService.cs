using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineExamination.BLL.Services
{
    public interface IAccountService
    {
        LoginViewModel Login(LoginViewModel vm);
        bool AddTeacher(UserViewModel vm);
        PagedResult<UserViewModel> GetAllTeachers(int pageSize, int pageNumber);
    }
}
