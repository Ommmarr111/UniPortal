namespace Day2MVC.Models
{
    public class CourseResultsViewModel
    {
        public string CourseName { get; set; }

        public int minDegree { get; set; }
        public List<TraineeResultVM> TraineeResults { get; set; }
    }
}
