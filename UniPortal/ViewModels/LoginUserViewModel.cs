using System.ComponentModel.DataAnnotations;

namespace Day2MVC.ViewModels
{
    public class LoginUserViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "*")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
