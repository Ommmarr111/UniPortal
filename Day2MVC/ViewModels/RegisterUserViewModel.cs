using System.ComponentModel.DataAnnotations;

namespace Day2MVC.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Name")]
        public string Userame { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }



    }
}
