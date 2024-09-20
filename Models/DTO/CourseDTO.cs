using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApi.Models
{
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}