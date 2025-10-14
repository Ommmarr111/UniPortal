using Day2MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;




namespace Day2MVC.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ApplicationDbContext context;

        public InstructorController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;   // default page = 1
            int pageSize = 5;             // how many records per page

            var instructors = context.Instructors
                                      .Include(i => i.Department) // if you need department
                                      .OrderBy(i => i.Id)
                                      .ToPagedList(pageNumber, pageSize);

            return View(instructors); // this will be IPagedList<Instructor>
        }

        public IActionResult Details(int id)

        {
            var instructor = context.Instructors.Include(ins => ins.Department).ToList();
            return View("Details", instructor);
        }
    }
}
