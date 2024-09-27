using Microsoft.EntityFrameworkCore;
using SchoolApi.Dto;
using SchoolApi.Data;
using SchoolApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

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
        return course != null ? CourseToDTO(course) : null;
    }

    public async Task CreateCourse(CourseDTO courseDTO)
    {
        var course = new Course
        {
            CourseID = courseDTO.CourseID,
            Title = courseDTO.Title,
            Credits = courseDTO.Credits
        };
        _context.Courses.Add(course);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (CourseExists(_context, course.CourseID))
            {
                ex.Data.Add("CourseAlreadyExist", course.CourseID);
            }
            throw;
        }
        //return TypedResults.Created($"/todoitems/{courseDTO.CourseID}", CourseToDTO(course));
    }
    // public async Task UpdateCourse(int id, CourseDTO courseDTO)
    // {
    //     if (id != courseDTO.CourseID)
    //         return TypedResults.BadRequest("Course ID mismatch");

    //     var course = await _context.Courses.FindAsync(id);

    //     if (course is null)
    //         return TypedResults.NotFound($"Course with ID {id} not found");

    //     course.Title = courseDTO.Title;
    //     course.Credits = courseDTO.Credits;

    //     await _context.SaveChangesAsync();

    //     return TypedResults.NoContent();
    // }
    // public async Task DeleteCourse(int id)
    // {
    //     if (await _context.Courses.FindAsync(id) is not Course course)
    //         return TypedResults.NotFound($"Course with ID {id} not found");

    //     _context.Courses.Remove(course);
    //     await _context.SaveChangesAsync();

    //     return TypedResults.NoContent();
    // }

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