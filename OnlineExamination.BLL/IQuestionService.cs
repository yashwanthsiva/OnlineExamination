using OnlineExamination.DataAccess;
using OnlineExamination.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamination.BLL.Services
{
    public interface IQuestionsService
    {
        PagedResult<QuestionsViewModel> GetAll(int pageNumber, int pageSize);
        Task<QuestionsViewModel> AddAsync(QuestionsViewModel QuestionsVM);
        IEnumerable<QuestionsViewModel> GetAllQuestionsByExam(int examId);
        bool IsExamAttended(int examId, int studentId);
    }
}