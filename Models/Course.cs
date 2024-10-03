using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApi.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        required public string Title { get; set; }
        public int Credits { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}