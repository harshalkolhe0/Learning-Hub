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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace LearningHubApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenAuthenticationFilter]
    public class EnrollmentsController : ControllerBase
    {
        private readonly LearningHubApiDbContext _context;

        public EnrollmentsController(LearningHubApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetEnrollment()
        {
            return await _context.Enrollment.ToListAsync();
        }
        // GET: api/Enrollments/{un}/myenrollments
        [HttpGet("{un}/myenrollments")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetMyEnrollment(string un)
        {
            var v = await _context.Enrollment.Include(m=>m.Course)
                                             .Where(m => m.UserID == un)
                                             .ToListAsync();
            return v;
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollment(int id)
        {
            var enrollment =  _context.Enrollment
                                            .Include(Enrollment=>Enrollment.Course )
                                              //  .ThenInclude()
                                            .Include(Enrollment=> Enrollment.User)
                                            .Where(p=>p.EnrollmentID==id)
                                            .FirstOrDefault();
            
            if (enrollment == null)
            {
                return NotFound();
            }

            return enrollment;
        }

        

        // PUT: api/Enrollments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return BadRequest();
            }
            //string user = TokenManager.getUser();
            //if (enrollment.UserID != user) return BadRequest();
            _context.Entry(enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
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

        // POST: api/Enrollments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Enrollment>> PostEnrollment(Enrollment enrollment)
        {
            //string user = TokenManager.getUser();
            //if(enrollment.UserID!=user)return BadRequest();
            _context.Enrollment.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrollment", new { id = enrollment.EnrollmentID }, enrollment);
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            
            var enrollment = await _context.Enrollment.FindAsync(id);
            
            if (enrollment == null)
            {
                return NotFound();
            }
            //string user = TokenManager.getUser();
            //if (enrollment.UserID != user) return BadRequest();
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollment.Any(e => e.EnrollmentID == id);
        }
    }
}
