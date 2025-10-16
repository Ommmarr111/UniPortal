namespace Day2MVC.Models // Or your ViewModel namespace
{
    public class DashboardViewModel
    {
        public int TotalInstructors { get; set; }
        public int TotalTrainees { get; set; }
        public int TotalCourses { get; set; }
        public double SuccessRate { get; set; }
        public List<RecentActivityResult> RecentActivity { get; set; } = new List<RecentActivityResult>();
    }

    // A helper class to hold clean data for the recent activity list
    public class RecentActivityResult
    {
        public string TraineeName { get; set; }
        public int TraineeId { get; set; }
        public string CourseName { get; set; }
        public bool Passed { get; set; }
    }
}