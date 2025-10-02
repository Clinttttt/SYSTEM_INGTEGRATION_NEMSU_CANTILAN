using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
    public class CourseDto
    {
        public int Cost { get; set; }
        public Guid? AdminId { get; set; }
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public int Unit { get; set; }
    
        public CategoryDto Category { get; set; } = null!;
    }

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
}
