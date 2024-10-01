using SchoolApi.Dto;

namespace SchoolApi.Services;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentDTO>> GetEnrollments();
    Task<EnrollmentDTO?> GetEnrollment(int id);
    Task CreateEnrollment(EnrollmentDTO enrollmentDTO);
    Task DeleteEnrollment(int id);
};