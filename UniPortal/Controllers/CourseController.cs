using Day2MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
