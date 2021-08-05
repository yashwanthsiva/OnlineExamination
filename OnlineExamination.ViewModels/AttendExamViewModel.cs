using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineExamination.ViewModels
{
    public class AttendExamViewModel
    {
        public int StudentId { get; set; }
        public string ExamName { get; set; }
        public List<QuestionsViewModel> Questions { get; set; }
        public string Message { get; set; }
    }
}
