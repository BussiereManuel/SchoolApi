using Microsoft.AspNetCore.Mvc;
using SchoolApi.Dto;
using SchoolApi.Services;
using Microsoft.IdentityModel.Tokens;

namespace SchoolApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    // GET: api/Enrollments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnrollmentDTO>>> GetEnrollments(IEnrollmentService service)
    {
        try
        {
            var enrollments = await service.GetEnrollments();
            return enrollments.IsNullOrEmpty() ? NotFound() : Ok(enrollments);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // GET: api/Enrollments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EnrollmentDTO>> GetEnrollment(int id, IEnrollmentService service)
    {
        try
        {
            var enrollment = await service.GetEnrollment(id);
            return enrollment is null ? NotFound() : Ok(enrollment);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // POST: api/Enrollments
    [HttpPost]
    public async Task<IActionResult> CreateEnrollment(EnrollmentDTO enrollmentDTO, IEnrollmentService service)
    {
        try
        {
            await service.CreateEnrollment(enrollmentDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Data.Contains("EnrollmentAlreadyExist"))
                return Conflict(ex.Data["EnrollmentAlreadyExist"]);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // DELETE: api/Enrollments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnrollment(int id, IEnrollmentService service)
    {
        try
        {
            await service.DeleteEnrollment(id);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Data.Contains("EnrollmentNotFound"))
                return NotFound(ex.Data["EnrollmentNotFound"]);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
