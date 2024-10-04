using Microsoft.EntityFrameworkCore;
using SchoolApi.Dto;
using SchoolApi.Data;
using SchoolApi.Models;
using AutoMapper;

namespace SchoolApi.Services;

public class CourseService : ICourseService
{
    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    public CourseService(SchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseDTO>> GetCourses()
    {
        return await _context.Courses.Select(course => _mapper.Map<CourseDTO>(course)).ToListAsync();
    }

    public async Task<CourseDTO?> GetCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        return course is Course ? _mapper.Map<CourseDTO>(course) : null;
    }

    public async Task CreateCourse(CourseDTO courseDTO)
    {
        try
        {
            var course = _mapper.Map<Course>(courseDTO);
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (await _context.Courses.FindAsync(courseDTO.CourseID) is Course)
                ex.Data.Add("CourseAlreadyExist", courseDTO.CourseID);
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
}