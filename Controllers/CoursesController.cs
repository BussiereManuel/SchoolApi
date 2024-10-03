using Microsoft.AspNetCore.Mvc;
using SchoolApi.Dto;
using SchoolApi.Services;
using Microsoft.IdentityModel.Tokens;

namespace SchoolApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    // GET: api/Courses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses(ICourseService courseService)
    {
        try
        {
            var course = await courseService.GetCourses();
            return course.IsNullOrEmpty() ? NotFound() : Ok(course);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // GET: api/Courses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDTO>> GetCourse(int id, ICourseService courseService)
    {
        try
        {
            var course = await courseService.GetCourse(id);
            return course == null ? NotFound() : Ok(course);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // POST: api/Courses
    [HttpPost]
    public async Task<IActionResult> CreateCourse(CourseDTO courseDTO, ICourseService courseService)
    {
        try
        {
            await courseService.CreateCourse(courseDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Data.Contains("CourseAlreadyExist"))
                return Conflict();
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // PUT: api/Courses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, CourseDTO courseDTO, ICourseService courseService)
    {
        if (id != courseDTO.CourseID)
            return BadRequest();

        try
        {
            await courseService.UpdateCourse(id, courseDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Data.Contains("CourseNotFound"))
                return NotFound();

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    // DELETE: api/Courses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id, ICourseService courseService)
    {
        try
        {
            await courseService.DeleteCourse(id);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Data.Contains("CourseNotFound"))
                return NotFound();

            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
