using SchoolApi.Dto;

namespace SchoolApi.Services;
public interface IStudentService
{
    Task<IEnumerable<StudentDTO>> GetStudents();
    Task<StudentDTO?> GetStudent(int id);
    Task CreateStudent(StudentDTO studentDTO);
    Task UpdateStudent(int id, StudentDTO studentDTO);
    Task DeleteStudent(int id);
};