namespace SchoolApi.Dto;

public class StudentDTO
{
    public int StudentID { get; set; }
    required public string LastName { get; set; }
    required public string FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }
}