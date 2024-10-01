using Microsoft.EntityFrameworkCore;
using SchoolApi.Dto;
using SchoolApi.Data;
using SchoolApi.Models;

namespace SchoolApi.Services;

public class StudentService : IStudentService
{
    private readonly SchoolContext _context;

    public StudentService(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentDTO>> GetStudents()
    {
        return await _context.Students.Select(x => StudentToDTO(x)).ToListAsync();
    }

    public async Task<StudentDTO?> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        return student is Student ? StudentToDTO(student) : null;
    }

    public async Task CreateStudent(StudentDTO studentDTO)
    {
        var student = new Student
        {
            LastName = studentDTO.LastName,
            FirstMidName = studentDTO.FirstMidName,
            EnrollmentDate = studentDTO.EnrollmentDate
        };
        try
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (StudentExists(_context, student.StudentID))
                ex.Data.Add("StudentAlreadyExist", student.StudentID);
            throw;
        }
    }

    public async Task UpdateStudent(int id, StudentDTO studentDTO)
    {
        if (await _context.Students.FindAsync(id) is not Student student)
            throw new Exception() { Data = { { "StudentNotFound", id } } };

        student.LastName = studentDTO.LastName;
        student.FirstMidName = studentDTO.FirstMidName;
        student.EnrollmentDate = studentDTO.EnrollmentDate;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteStudent(int id)
    {
        if (await _context.Students.FindAsync(id) is not Student student)
            throw new Exception() { Data = { { "StudentNotFound", id } } };

        try
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static StudentDTO StudentToDTO(Student student) =>
        new StudentDTO
        {
            StudentID = student.StudentID,
            LastName = student.LastName,
            FirstMidName = student.FirstMidName,
            EnrollmentDate = student.EnrollmentDate
        };

    public static bool StudentExists(SchoolContext context, int id)
    {
        return context.Students.Any(e => e.StudentID == id);
    }

}