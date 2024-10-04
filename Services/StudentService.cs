using Microsoft.EntityFrameworkCore;
using SchoolApi.Dto;
using SchoolApi.Data;
using SchoolApi.Models;
using AutoMapper;

namespace SchoolApi.Services;

public class StudentService : IStudentService
{
    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    public StudentService(SchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDTO>> GetStudents()
    {
        return await _context.Students.Select(student => _mapper.Map<StudentDTO>(student)).ToListAsync();
    }

    public async Task<StudentDTO?> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        return student is Student ? _mapper.Map<StudentDTO>(student) : null;
    }

    public async Task CreateStudent(StudentDTO studentDTO)
    {
        try
        {
            var student = _mapper.Map<Student>(studentDTO);
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (await _context.Students.FindAsync(studentDTO.StudentID) is Student)
                ex.Data.Add("StudentAlreadyExist", studentDTO.StudentID);
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
}