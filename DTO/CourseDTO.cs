using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApi.Dto
{
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}