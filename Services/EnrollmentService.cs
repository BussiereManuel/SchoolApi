using Microsoft.EntityFrameworkCore;
using SchoolApi.Dto;
using SchoolApi.Data;
using SchoolApi.Models;

namespace SchoolApi.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly SchoolContext _context;

    public EnrollmentService(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EnrollmentDTO>> GetEnrollments()
    {
        return await _context.Enrollments.Select(x => EnrollmentToDTO(x)).ToListAsync();
    }

    public async Task<EnrollmentDTO?> GetEnrollment(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        return enrollment is Enrollment ? EnrollmentToDTO(enrollment) : null;
    }

    public async Task CreateEnrollment(EnrollmentDTO enrollmentDTO)
    {
        var enrollment = new Enrollment
        {
            CourseID = enrollmentDTO.CourseID,
            StudentID = enrollmentDTO.StudentID,
            Grade = enrollmentDTO.Grade
        };
        try
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (EnrollmentExists(enrollment.EnrollmentID))
                ex.Data.Add("EnrollmentAlreadyExist", enrollment.EnrollmentID);
            throw;
        }
    }

    public async Task DeleteEnrollment(int id)
    {
        if (await _context.Enrollments.FindAsync(id) is not Enrollment enrollment)
            throw new Exception() { Data = { { "EnrollmentNotFound", id } } };

        try
        {
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static EnrollmentDTO EnrollmentToDTO(Enrollment enrollment) =>
        new EnrollmentDTO
        {
            EnrollmentID = enrollment.EnrollmentID,
            CourseID = enrollment.CourseID,
            StudentID = enrollment.StudentID,
            Grade = enrollment.Grade
        };

    private bool EnrollmentExists(int id)
    {
        return _context.Enrollments.Any(e => e.EnrollmentID == id);
    }

}