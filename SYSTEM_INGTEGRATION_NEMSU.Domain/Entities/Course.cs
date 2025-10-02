﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public double Cost { get; set; }
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public int Unit { get; set; }
        public ICollection<EnrollmentCourse> Enrollments { get; set; } = new List<EnrollmentCourse>();

        public Guid? CategoryId { get; set; }   
       public Category Category { get; set; } = null!;
    }
   
}
