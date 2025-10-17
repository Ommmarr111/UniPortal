using Day2MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day2MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext _context)
        {
            _logger = logger;
            context = _context;
        }

        public IActionResult Index()
        {
            var totalTrainees = context.Trainees.Count();
            var totalCourses = context.Courses.Count();
            var totalInstructors = context.Instructors.Count();

            var totalResults = context.CrsResults.Count();
            var passedResults = context.CrsResults
                                        .Include(cr => cr.Course)
                                        .Count(cr => cr.Degree >= cr.Course.minDegree);

            var viewModel = new DashboardViewModel
            {
                TotalTrainees = totalTrainees,
                TotalCourses = totalCourses,
                TotalInstructors = totalInstructors,
                SuccessRate = totalResults > 0 ? ((double)passedResults / totalResults) * 100 : 0
            };

            // --- Get Recent Activity (e.g., the last 5 course results added) ---
            var recentResults = context.CrsResults
                .OrderByDescending(cr => cr.Id) // Assuming higher ID = newer
                .Take(5)
                .Select(cr => new RecentActivityResult
                {
                    TraineeName = cr.Trainee.Name,
                    TraineeId = cr.TraineeId,
                    CourseName = cr.Course.Name,
                    Passed = cr.Degree >= cr.Course.minDegree
                }).ToList();

            viewModel.RecentActivity = recentResults;
            // --- NEW: Query for the Trainees by Department Chart ---
            var traineesByDept = context.Departments
                .Include(d => d.Trainees) // Make sure your Department model has a List<Trainee>
                .Select(d => new DepartmentTraineeCount
                {
                    DepartmentName = d.Name,
                    TraineeCount = d.Trainees.Count()
                })
                .OrderByDescending(d => d.TraineeCount)
                .ToList();

            viewModel.TraineesPerDepartment = traineesByDept;

            return View(viewModel);
        }
    }
}
