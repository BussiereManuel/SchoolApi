using Microsoft.EntityFrameworkCore;
using SchoolApi.Dto;
using SchoolApi.Data;
using SchoolApi.Models;
using AutoMapper;

namespace SchoolApi.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    public EnrollmentService(SchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EnrollmentDTO>> GetEnrollments()
    {
        return await _context.Enrollments.Select(enrollment => _mapper.Map<EnrollmentDTO>(enrollment)).ToListAsync();
    }

    public async Task<EnrollmentDTO?> GetEnrollment(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        return enrollment is Enrollment ? _mapper.Map<EnrollmentDTO>(enrollment) : null;
    }

    public async Task CreateEnrollment(EnrollmentDTO enrollmentDTO)
    {
        try
        {
            var enrollment = _mapper.Map<Enrollment>(enrollmentDTO);
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            if (await _context.Enrollments.FindAsync(enrollmentDTO.EnrollmentID) is Enrollment)
                ex.Data.Add("EnrollmentAlreadyExist", enrollmentDTO.EnrollmentID);
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
}