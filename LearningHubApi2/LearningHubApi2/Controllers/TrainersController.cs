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

namespace LearningHubApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TokenAuthenticationFilter]
    public class TrainersController : ControllerBase
    {
        private readonly LearningHubApiDbContext _context;

        public TrainersController(LearningHubApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Trainers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainer()
        {
            return await _context.Trainer.ToListAsync();
        }

        // GET: api/Trainers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trainer>> GetTrainer(int id)
        {
            
            var trainer = await _context.Trainer.FindAsync(id);
            
            if (trainer == null)
            {
                return NotFound();
            }

            return trainer;
        }

        // PUT: api/Trainers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainer(int id, Trainer trainer)
        {
            //string un = TokenManager.getUser();
            //if (trainer.UserId != un) return BadRequest();
            if (id != trainer.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(trainer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainerExists(id))
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

        // POST: api/Trainers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trainer>> PostTrainer(Trainer trainer)
        {
            //string un = TokenManager.getUser();
            //if (trainer.UserId != un) return BadRequest();


            _context.Trainer.Add(trainer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrainerExists(trainer.CourseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTrainer", new { id = trainer.CourseId }, trainer);
        }

        // DELETE: api/Trainers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var trainer = await _context.Trainer.FindAsync(id);
            if (trainer == null)
            {
                return NotFound();
            }
            //string un = TokenManager.getUser();
            //if (trainer.UserId != un) return BadRequest();

            _context.Trainer.Remove(trainer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainerExists(int id)
        {
            return _context.Trainer.Any(e => e.CourseId == id);
        }
    }
}
