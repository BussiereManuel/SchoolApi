using SchoolApi.Models;
using Microsoft.EntityFrameworkCore;

namespace SchoolApi.DAL
{
    public class SchoolContext : DbContext
    {
        //  Constructor
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}