using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<EnrollmentCourse> enrollcourse { get; set; }
        public DbSet<Invoice> invoice { get; set; }
        public DbSet<InstructorAnnouncement> announcements { get; set; }
        public DbSet<StudentProfile> studentprofiles { get; set; }
        public DbSet<FacilitatorProfile> facilitatorprofiles { get; set; }
    }
}
