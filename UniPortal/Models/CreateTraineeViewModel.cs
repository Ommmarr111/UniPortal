namespace UniPortal.Models
{
    public class CreateTraineeViewModel
    {
        // Only include properties that the form will provide.
        public string Name { get; set; }
        public string Address { get; set; }
        public string? Image { get; set; } // Make string properties that can be empty nullable
        public string Grade { get; set; }
        public int DepartmentId { get; set; }
    }
}
