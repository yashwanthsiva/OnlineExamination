using OnlineExamination.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineExamination.ViewModels
{
    public class QuestionsViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Exam")]
        public int ExamsId { get; set; }
        [Required]
        [Display(Name ="Question")]
        public string Question { get; set; }
        [Required]
        [Display(Name = "Answer")]
        public int Answer { get; set; }
        [Required]
        [Display(Name = "Option 1")]
        public string Option1 { get; set; }
        [Required]
        [Display(Name = "Option 2")]
        public string Option2 { get; set; }
        [Required]
        [Display(Name = "Option 3")]
        public string Option3 { get; set; }
        [Required]
        [Display(Name = "Option 4")]
        public string Option4 { get; set; }
        public List<QuestionsViewModel> QuestionsList { get; set; }
        public IEnumerable<Exams> ExamList { get; set; }
        public int TotalCount { get; set; }
        public int SelectedAnswer { get; set; }
        public Questions QuestionsId { get; set; }

        public QuestionsViewModel(Questions model)
        {
            Id = model.Id;
            ExamsId = model.ExamsId;
            Question = model.Question ?? "";
            Option1 = model.Option1 ?? "";
            Option2 = model.Option2 ?? "";
            Option3 = model.Option3 ?? "";
            Option4 = model.Option4 ?? "";
            Answer = model.Answer;

        }
        public Questions ConvertViewModel(QuestionsViewModel vm)
        {
            return new Questions
            {
                Id = vm.Id,
            ExamsId = vm.ExamsId,
            Question = vm.Question ?? "",
            Option1 = vm.Option1 ?? "",
            Option2 = vm.Option2 ?? "",
            Option3 = vm.Option3 ?? "",
            Option4 = vm.Option4 ?? "",
            Answer = vm.Answer,
        };

        }
        
    }
}
