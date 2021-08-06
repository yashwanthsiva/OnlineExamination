using OnlineExamination.DataAccess;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.BLL.Services
{
    public interface IGroupService
    {
        PagedResult<GroupViewModel> GetAllGroups(int pagenumber, int pagesize);
        Task<GroupViewModel> AddGroupAsync(GroupViewModel groupVM);
        IEnumerable<Groups> GetAllGroups();
        GroupViewModel GetById(int groupId);
    }
}
