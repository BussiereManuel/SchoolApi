using SchoolApi.Dto;

namespace SchoolApi.Services;
public interface ICourseService
{
    Task<IEnumerable<CourseDTO>> GetCourses();
    Task<CourseDTO?> GetCourse(int id);
    Task CreateCourse(CourseDTO courseDTO);
    // Task UpdateCourse(int id, CourseDTO courseDTO);
    // Task DeleteCourse(int id);
}