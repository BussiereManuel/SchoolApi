// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;

using Microsoft.AspNetCore.Mvc;
using SchoolApi.Data;
using SchoolApi.Dto;
using SchoolApi.Services;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public EnrollmentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDTO>>> GetEnrollments()
        {
            return await _context.Enrollments.Select(x => EnrollmentToDTO(x)).ToListAsync();
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDTO>> GetEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return EnrollmentToDTO(enrollment);
        }

        // POST: api/Enrollments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EnrollmentDTO>> PostEnrollment(EnrollmentDTO enrollmentDTO)
        {

            var enrollment = new Enrollment
            {
                CourseID = enrollmentDTO.CourseID,
                StudentID = enrollmentDTO.StudentID,
                Grade = enrollmentDTO.Grade
            };

            _context.Enrollments.Add(enrollment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (!CourseService.CourseExists(_context, enrollment.CourseID) || !StudentsController.StudentExists(_context, enrollment.StudentID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEnrollment", new { id = enrollment.EnrollmentID }, enrollmentDTO);
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentID == id);
        }

        private static EnrollmentDTO EnrollmentToDTO(Enrollment enrollment) =>
            new EnrollmentDTO
            {
                EnrollmentID = enrollment.EnrollmentID,
                CourseID = enrollment.CourseID,
                StudentID = enrollment.StudentID,
                Grade = enrollment.Grade
            };
    }
}
