namespace Day2MVC.Models // Or your ViewModel namespace
{
    public class DashboardViewModel
    {
        public int TotalInstructors { get; set; }
        public int TotalTrainees { get; set; }
        public int TotalCourses { get; set; }
        public double SuccessRate { get; set; }
        public List<RecentActivityResult> RecentActivity { get; set; } = new List<RecentActivityResult>();

        // NEW: Add a property to hold data for the chart
        public List<DepartmentTraineeCount> TraineesPerDepartment { get; set; }
    }
    // Helper class for the new chart data
    public class DepartmentTraineeCount
    {
        public string DepartmentName { get; set; }
        public int TraineeCount { get; set; }
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