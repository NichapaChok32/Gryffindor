using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gryffindor.Models;
using Microsoft.Extensions.Logging;

namespace Gryfindor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulePlansController : ControllerBase
    {
        private readonly OrderProjectContext _context;
        
        public SchedulePlansController(OrderProjectContext context)
        {
            _context = context;
        }

        // GET: api/SchedulePlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchedulePlan>>> GetSchedulePlan()
        {
            return await _context.SchedulePlan.ToListAsync();
        }

        // GET: api/SchedulePlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchedulePlan>> GetSchedulePlan(int id)
        {
            var schedulePlan = await _context.SchedulePlan.FindAsync(id);

            if (schedulePlan == null)
            {
                return NotFound();
            }

            return schedulePlan;
        }

        // PUT: api/SchedulePlans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedulePlan(int id, SchedulePlan schedulePlan)
        {
            if (id != schedulePlan.ScheduleId)
            {
                return BadRequest();
            }

            _context.Entry(schedulePlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchedulePlanExists(id))
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

        // POST: api/SchedulePlans
        [HttpPost]
        public async Task<ActionResult<SchedulePlan>> PostSchedulePlan(SchedulePlan schedulePlan)
        {
            _context.SchedulePlan.Add(schedulePlan);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchedulePlanExists(schedulePlan.ScheduleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetSchedulePlan", new { id = schedulePlan.ScheduleId }, schedulePlan);
        }

        // DELETE: api/SchedulePlans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SchedulePlan>> DeleteSchedulePlan(int id)
        {
            var schedulePlan = await _context.SchedulePlan.FindAsync(id);
            if (schedulePlan == null)
            {
                return NotFound();
            }

            _context.SchedulePlan.Remove(schedulePlan);
            await _context.SaveChangesAsync();

            return schedulePlan;
        }

        private bool SchedulePlanExists(int id)
        {
            return _context.SchedulePlan.Any(e => e.ScheduleId == id);
        }
    }
}
