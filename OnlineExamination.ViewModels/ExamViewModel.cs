using OnlineExamination.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineExamination.ViewModels
{
    public class ExamViewModel
    {
        public ExamViewModel()
        {

        }
        public int Id { get; set; }
        [Required]
        [Display(Name ="Exam Name")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public int GroupsID { get; set; }
        public List<ExamViewModel> ExamList { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<Groups>GroupList { get; set; }
        public ExamViewModel (Exams model)
        {
            Id = model.Id;
            Title = model.Title ?? "";
            Description = model.Description ?? "";
            StartDate = model.StartDate;
            Time = model.Time;
            GroupsID = model.GroupsID;
        }
        public Exams CovertViewModel(ExamViewModel vm)
        {
            return new Exams
            {
                Id = vm.Id,
                Title = vm.Title ?? "",
                Description = vm.Description ?? "",
            StartDate = vm.StartDate,
            Time = vm.Time,
            GroupsID = vm.GroupsID,
        };
        }
    }
}
