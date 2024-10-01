using Microsoft.AspNetCore.Mvc;
// using SchoolApi.Data;
using SchoolApi.Dto;
using SchoolApi.Services;
using Microsoft.IdentityModel.Tokens;

namespace SchoolApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    // private readonly SchoolContext _context;

    // public StudentsController(SchoolContext context)
    // {
    //     _context = context;
    // }

    // GET: api/Students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents(IStudentService studentService)
    {
        try
        {
            var student = await studentService.GetStudents();
            return student.IsNullOrEmpty() ? NotFound() : Ok(student);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // GET: api/Students/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> GetStudent(int id, IStudentService studentService)
    {
        try
        {
            var student = await studentService.GetStudent(id);
            return student == null ? NotFound() : Ok(student);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // POST: api/Students
    [HttpPost]
    public async Task<ActionResult<StudentDTO>> CreateStudent(StudentDTO studentDTO, IStudentService studentService)
    {
        try
        {
            await studentService.CreateStudent(studentDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Data.Contains("StudentAlreadyExist"))
                return Conflict(ex.Data["StudentAlreadyExist"]);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // PUT: api/Students/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, StudentDTO studentDTO, IStudentService studentService)
    {
        if (id != studentDTO.StudentID)
            return BadRequest();

        try
        {
            await studentService.UpdateStudent(id, studentDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Data.Contains("StudentNotFound"))
                return NotFound();

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // DELETE: api/Students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id, IStudentService studentService)
    {
        try
        {
            await studentService.DeleteStudent(id);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Data.Contains("StudentNotFound"))
                return NotFound();
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
