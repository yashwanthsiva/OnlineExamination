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
    public class QuestionsService : IQuestionsService
    {
        IUnitOfWork _unitOfWork;
        ILogger<QuestionsService> _iLogger;

        public QuestionsService(IUnitOfWork unitOfWork, ILogger<QuestionsService> iLogger)
        {
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }

        public async Task<QuestionsViewModel> AddAsync(QuestionsViewModel QuestionsVM)
        {
            try
            {
                Questions objGroup = QuestionsVM.ConvertViewModel(QuestionsVM);
                await _unitOfWork.GenericRepository<Questions>().AddAsync(objGroup);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                return null;
            }
            return QuestionsVM;
        }

        public PagedResult<QuestionsViewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new QuestionsViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<QuestionsViewModel> detailList = new List<QuestionsViewModel>();
                var modelList = _unitOfWork.GenericRepository<Questions>().GetAll().Skip(ExcludeRecords)
                    .Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<Questions>().GetAll().ToList();
                detailList = ListInfo(modelList);
                if (detailList != null)
                {
                    model.QuestionsList = detailList;
                    model.TotalCount = totalCount.Count();
                }
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            var result = new PagedResult<QuestionsViewModel>
            {
                Data = model.QuestionsList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<QuestionsViewModel> ListInfo(List<Questions> modelList)
        {
            return modelList.Select(o => new QuestionsViewModel(o)).ToList();
        }

        public IEnumerable<QuestionsViewModel> GetAllQuestionsByExam(int examId)
        {
            try
            {
                var QuestionsList = _unitOfWork.GenericRepository<Questions>().GetAll().Where(x => x.ExamsId == examId); ;
                return ListInfo(QuestionsList.ToList());

            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message);
            }
            return Enumerable.Empty<QuestionsViewModel>();
        }

        public bool IsExamAttended(int examId, int studentId)
        {
            try
            {
                var questionsRecord = _unitOfWork.GenericRepository<ExamResults>().GetAll()
                    .FirstOrDefault(x => x.ExamsID == examId && x.StudentsId == studentId);
                return questionsRecord == null ? false : true;
            }
            catch (Exception ex)
            {

                _iLogger.LogError(ex.Message);
            }
            return false;
        }
    }
}
