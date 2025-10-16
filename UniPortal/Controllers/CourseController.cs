using Day2MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace Day2MVC.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext context;

        public CourseController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Details()
        {
            var Courses = context.Courses.ToList();
            return View("Details", Courses);
        }
        public IActionResult Index(string searchString, int? departmentId, int? page)
        {
            // --- For the Filter Dropdown ---
            // We pass the list of all departments to the view.
            ViewBag.Departments = context.Departments.ToList();
            // We also pass back the current search/filter values to keep the form populated.
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentDept"] = departmentId;

            // Start with all courses
            var courses = context.Courses.Include(c => c.Department)
                                         .Include(c => c.CrsResults) // Include results to count trainees
                                         .AsQueryable();

            // --- Apply Search Filter ---
            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Name.Contains(searchString));
            }

            // --- Apply Department Filter ---
            if (departmentId.HasValue)
            {
                courses = courses.Where(c => c.DepartmentId == departmentId.Value);
            }

            // --- Paginate the Filtered Results ---
            int pageNumber = page ?? 1;
            int pageSize = 5;

            return View(courses.OrderBy(c => c.Name).ToPagedList(pageNumber, pageSize));
        }

        public IActionResult ShowResult(int id)
        {
            var courseResults = context.CrsResults
                                .Include(cr => cr.Trainee)
                                .Include(cr => cr.Course)
                                .Where(cr => cr.CourseId == id)
                                .ToList();

            if (!courseResults.Any())
            {
                return Content($"No results found for course ID {id}");
            }
            var course = courseResults.FirstOrDefault()?.Course;

            var viewModel = new CourseResultsViewModel

            {
                CourseName = course.Name,
                minDegree = course.minDegree,
                TraineeResults = courseResults
                    .Select(cr => new TraineeResultVM
                    {
                        TraineeName = cr.Trainee.Name,
                        Result = cr.Degree,
                        Status = cr.Degree >= course.minDegree ? "Passed" : "Failed"

                    }).ToList()
            };

            return View(viewModel);

        }
    }
}
