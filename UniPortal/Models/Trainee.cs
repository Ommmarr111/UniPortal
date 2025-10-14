using System.ComponentModel.DataAnnotations.Schema;

namespace Day2MVC.Models
{
    public class Trainee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }

        public string Grade { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<CrsResult> CrsResults { get; set; }
    }
}
