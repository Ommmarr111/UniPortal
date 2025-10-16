using Day2MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniPortal.Models;
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
            var courseResult = context.CrsResults
                .Include(cr => cr.Trainee)
                .Include(cr => cr.Course)
                .FirstOrDefault(cr => cr.TraineeId == id && cr.CourseId == crsId);

            // 3. Use NotFound() for better error handling
            if (courseResult == null)
            {
                return NotFound("The specified result for this trainee and course was not found.");
            }

            // 2. Use a strongly-typed ViewModel instead of ViewBag
            var viewModel = new TraineeCourseResultVM
            {
                TraineeName = courseResult.Trainee.Name,
                CourseName = courseResult.Course.Name,
                Degree = courseResult.Degree,
                IsPassing = courseResult.Degree >= courseResult.Course.minDegree
            };

            return View("ShowResult", viewModel);
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

        // GET: Trainee/Details/5
        public IActionResult Details(int id)
        {
            // Find the trainee by their ID.
            // We must .Include() the related data that we want to display in the view.
            // We also use .ThenInclude() to get the Course name from the CrsResult.
            var trainee = context.Trainees
                .Include(t => t.Department)
                .Include(t => t.CrsResults)
                    .ThenInclude(cr => cr.Course)
                .FirstOrDefault(t => t.Id == id);

            // If no trainee is found with that ID, return a 404 Not Found error.
            if (trainee == null)
            {
                return NotFound();
            }

            // Pass the found trainee object to the view.
            return View(trainee);
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

        // GET: /Trainee/Create
        public IActionResult Create()
        {
            ViewBag.Departments = context.Departments.ToList();
            // We don't need to pass a model, the view will create an empty form
            return View();
        }

        // POST: /Trainee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // The parameter is now the ViewModel
        public IActionResult Create(CreateTraineeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Manually map the data from the ViewModel to a new Trainee entity
                var newTrainee = new Trainee
                {
                    Name = viewModel.Name,
                    Address = viewModel.Address,
                    Image = viewModel.Image,
                    Grade = viewModel.Grade,
                    DepartmentId = viewModel.DepartmentId
                };

                context.Trainees.Add(newTrainee);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            // If validation fails, repopulate the dropdown and return the view
            ViewBag.Departments = context.Departments.ToList();
            return View(viewModel);
        }

    }
}

