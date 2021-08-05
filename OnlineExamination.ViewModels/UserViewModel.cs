using OnlineExamination.DataAccess;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamination.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(Users model)
        {
            Id = model.Id;
            Name = model.Name ?? "";
            UserName = model.Username;
            Password = model.Password;
            Role = model.Role;
        }
        public Users ConvertViewModel(UserViewModel vm)
        {
            return new Users
            { Id = vm.Id,
            Name = vm.Name ?? "",
            Username = vm.UserName,
            Password = vm.Password,
            Role = vm.Role,
        };

        }
        public int Id { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
