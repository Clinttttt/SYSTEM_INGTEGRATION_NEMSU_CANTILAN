using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
    new Category{Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),Name = "Mathematics",Icon = "📐",Color = "teal"},
    new Category{Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),Name = "Arts & Humanities",Icon = "🎨",Color = "purple"},
    new Category{Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),Name = "Computer Science",Icon = "💻",Color = "blue"},
    new Category{Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),Name = "Social Sciences",Icon = "📚",Color = "orange"},
    new Category{Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),Name = "Natural Sciences",Icon = "⚗️",Color = "gray"}
);
        }
        public DbSet<User> users { get; set; }
        public DbSet<Course> course { get; set; }
        public DbSet<EnrollmentCourse> enrollcourse { get; set; }
        public DbSet<Invoice> invoice { get; set; }
        public DbSet<InstructorAnnouncement> announcements { get; set; }
        public DbSet<StudentProfile> studentprofiles { get; set; }
        public DbSet<FacilitatorProfile> facilitatorprofiles { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<AcademicInformation> academicInformation { get; set; }
        public DbSet<ContactInformation> contactInformation { get; set; }
        public DbSet<PersonalInformation> personalInformation { get; set; }
    }
}
