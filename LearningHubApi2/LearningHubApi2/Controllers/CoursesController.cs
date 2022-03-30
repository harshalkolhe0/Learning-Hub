#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningHubApi2.Data_Layer;
using LearningHubApi2.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace LearningHubApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenAuthenticationFilter]

    public class CoursesController : ControllerBase
    {
        private readonly LearningHubApiDbContext _context;

        public CoursesController(LearningHubApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            var v = await _context.Course.Include(m => m.Trainer)
                                             .ToListAsync();
            return v;
            //return await _context.Course.ToListAsync();
        }

        // GET: api/Courses/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = _context.Course.Include(c=>c.Trainer).Where(p=>p.ID==id).FirstOrDefault();
            //string user = TokenManager.getUser();
            //var t = _context.Trainer.Where(t => t.CourseId == id && t.UserId == user).Any();
            //if (!t) return BadRequest();
            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCourse(int id, StringCourse ncourse)
        {
            //string user = TokenManager.getUser();
            //var t = _context.Trainer.Where(t => t.CourseId == id && t.UserId == user).Any();
            //if (!t) return BadRequest();
            Course course = new Course();
            try
            {
                course = ncourse.ToCourse();
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
            if (id != course.ID)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(StringCourse course)
        {
            //string user = TokenManager.getUser();
            //var t = _context.Trainer.Where(t => t.CourseId == course.ID && t.UserId == user).Any();
            //if (!t) return BadRequest();
            Course ncourse = new Course();
            try
            {
                 ncourse= course.ToCourse();
            }
            catch(Exception exp)
            {
                return BadRequest();
            }
            
            _context.Course.Add(ncourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.ID }, ncourse);
        }

        // DELETE: api/Courses/5
        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            //string user = TokenManager.getUser();
            //var t = _context.Trainer.Where(t => t.CourseId == id && t.UserId == user).Any();
            //if (!t) return BadRequest();
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.ID == id);
        }
    }
}
