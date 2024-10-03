namespace SchoolApi.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        required public string LastName { get; set; }
        required public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}