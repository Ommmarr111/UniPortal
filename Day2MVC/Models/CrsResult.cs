using System.ComponentModel.DataAnnotations.Schema;

namespace Day2MVC.Models
{
    public class CrsResult
    {
        public int Id { get; set; }

        public int Degree { get; set; }

        [ForeignKey("Trainee")]
        public int TraineeId { get; set; }
        public Trainee Trainee { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
