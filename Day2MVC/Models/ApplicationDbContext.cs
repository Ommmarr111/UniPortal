using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Day2MVC.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CrsResult> CrsResults { get; set; }

        //public ApplicationDbContext() : base()
        //{

        //}
        public ApplicationDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

    }
}
