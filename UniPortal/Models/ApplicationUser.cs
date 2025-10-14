using Microsoft.AspNetCore.Identity;

namespace Day2MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
    }
}
