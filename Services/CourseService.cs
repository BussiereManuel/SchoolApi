using Microsoft.EntityFrameworkCore;
using SchoolApi.Dto;
using SchoolApi.Data;
using SchoolApi.Models;

namespace SchoolApi.Services;

public class CourseService : ICourseService
{
    private readonly SchoolContext _context;

    public CourseService(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseDTO>> GetCourses()
    {
        return await _context.Courses.Select(x => CourseToDTO(x)).ToListAsync();
    }

    public async Task<CourseDTO?> GetCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        return course is Course ? CourseToDTO(course) : null;
    }

    public async Task CreateCourse(CourseDTO courseDTO)
    {
        var course = new Course
        {
            CourseID = courseDTO.CourseID,
            Title = courseDTO.Title,
            Credits = courseDTO.Credits
        };
        try
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (CourseExists(_context, course.CourseID))
                ex.Data.Add("CourseAlreadyExist", course.CourseID);
            throw;
        }
    }

    public async Task UpdateCourse(int id, CourseDTO courseDTO)
    {
        if (await _context.Courses.FindAsync(id) is not Course course)
            throw new Exception() { Data = { { "CourseNotFound", id } } };

        course.Title = courseDTO.Title;
        course.Credits = courseDTO.Credits;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteCourse(int id)
    {
        if (await _context.Courses.FindAsync(id) is not Course course)
            throw new Exception() { Data = { { "CourseNotFound", id } } };

        try
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static CourseDTO CourseToDTO(Course course) =>
        new CourseDTO
        {
            CourseID = course.CourseID,
            Title = course.Title,
            Credits = course.Credits
        };

    public static bool CourseExists(SchoolContext _context, int id)
    {
        return _context.Courses.Any(e => e.CourseID == id);
    }
}