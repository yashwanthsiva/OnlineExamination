using OnlineExamination.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineExamination.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {

        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Contact No")]
        public string Contact { get; set; }
        public int? GroupsId { get; set; }
        public int TotalCount { get; set; }
        public List<StudentViewModel> StudentList { get; set; }
        public StudentViewModel(Students model)
        {
            Id = model.Id;
            Name = model.Name ?? "";
            UserName = model.Username;
            Password = model.Password;
            Contact = model.Contact ?? "";
            GroupsId = model.GroupsId;
        }
        public Students ConvertViewModel(StudentViewModel vm)
        {
            return new Students
            {
                Id = vm.Id,
                Name = vm.Name ?? "",
                Username = vm.UserName,
                Password = vm.Password,
                Contact = vm.Contact??"",
            GroupsId = vm.GroupsId
        };
        }
    }
}
