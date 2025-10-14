﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Day2MVC.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Degree { get; set; }

        public int minDegree { get; set; }

        public int Hours { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<CrsResult> CrsResults { get; set; }

        public List<Instructor> Instructors { get; set; }
    }
}
