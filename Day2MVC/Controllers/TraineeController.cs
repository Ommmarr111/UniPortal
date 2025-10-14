using Day2MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList.Extensions;

namespace Day2MVC.Controllers
{
    public class TraineeController : Controller
    {
        private readonly ApplicationDbContext context;

        public TraineeController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult ShowResult(int id, int crsId)
        {
            var trainee = context.Trainees.Include(r => r.CrsResults).FirstOrDefault(tr => tr.Id == id);
            if (trainee != null)
            {
                ViewBag.CourseResult = trainee.CrsResults.FirstOrDefault(r => r.CourseId == crsId);
                if (ViewBag.CourseResult != null)
                {
                    return View("ShowResult", trainee);
                }
                else
                {
                    return Content("Result not found for the specified course.");
                }
            }
            else
            {
                return Content("Trainee not found.");
            }

        }

        public IActionResult TraineeResult(int id)
        {
            var traineeResults = context.CrsResults
                   .Where(cr => cr.TraineeId == id)
                   .Include(cr => cr.Course)   // ✅ because we want Course.Name + MinDegree
                   .Include(cr => cr.Trainee)  // optional, if you want trainee name too
                   .ToList();
            if (!traineeResults.Any())
            {
                ViewBag.TraineeId = id;
                return View("TraineeNotFound");
            }
            var trainee = traineeResults.FirstOrDefault()?.Trainee;
            var vm = new TraineeWithResultsVM
            {
                TraineeName = trainee.Name,
                Results = traineeResults.Select(cr => new CourseResultVM
                {
                    CourseName = cr.Course.Name,
                    Degree = cr.Degree,
                    minDegree = cr.Course.minDegree,
                    Status = cr.Degree >= cr.Course.minDegree ? "Passed" : "Failed"
                }).ToList()
            };

            return View("ShowTraineeResults", vm);
        }
        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;   // default page = 1
            int pageSize = 5;             // how many records per page

            var trainees = context.Trainees
                                      .Include(i => i.Department) // if you need department
                                      .OrderBy(i => i.Id)
                                      .ToPagedList(pageNumber, pageSize);

            return View(trainees); // this will be IPagedList<Instructor>
        }
    }
}
