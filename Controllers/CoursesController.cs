// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolApi.Data;
using SchoolApi.Dto;
using SchoolApi.Services;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly SchoolContext _context;
        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

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
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCourse(CourseDTO courseDTO, ICourseService courseService)
        {
            try
            {
                await courseService.CreateCourse(courseDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Data.Contains("CourseAlreadyExist"))
                {
                    return Conflict();
                }
                return StatusCode(500, ex.Message);
            }
        }

        // // PUT: api/Courses/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IResult> PutCourse(int id, CourseDTO courseDTO, ICourseService courseService)
        // {
        //     return await courseService.UpdateCourse(id, courseDTO);
        // }

        // // DELETE: api/Courses/5
        // [HttpDelete("{id}")]
        // public async Task<IResult> DeleteCourse(int id, ICourseService courseService)
        // {
        //     return await courseService.DeleteCourse(id);
        // }
    }
}
