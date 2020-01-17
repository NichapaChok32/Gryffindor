using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gryffindor.Models;

namespace Gryfindor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateMountsController : ControllerBase
    {
        private readonly OrderProjectContext _context;

        public DateMountsController(OrderProjectContext context)
        {
            _context = context;
        }

        // GET: api/DateMounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateMount>>> GetDateMount()
        {
            return await _context.DateMount.ToListAsync();
        }

        // GET: api/DateMounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DateMount>> GetDateMount(int id)
        {
            var dateMount = await _context.DateMount.FindAsync(id);

            if (dateMount == null)
            {
                return NotFound();
            }

            return dateMount;
        }

        // PUT: api/DateMounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDateMount(int id, DateMount dateMount)
        {
            if (id != dateMount.MonthId)
            {
                return BadRequest();
            }

            _context.Entry(dateMount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateMountExists(id))
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

        // POST: api/DateMounts
        [HttpPost]
        public async Task<ActionResult<DateMount>> PostDateMount(DateMount dateMount)
        {
            _context.DateMount.Add(dateMount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DateMountExists(dateMount.MonthId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDateMount", new { id = dateMount.MonthId }, dateMount);
        }

        // DELETE: api/DateMounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DateMount>> DeleteDateMount(int id)
        {
            var dateMount = await _context.DateMount.FindAsync(id);
            if (dateMount == null)
            {
                return NotFound();
            }

            _context.DateMount.Remove(dateMount);
            await _context.SaveChangesAsync();

            return dateMount;
        }

        private bool DateMountExists(int id)
        {
            return _context.DateMount.Any(e => e.MonthId == id);
        }
    }
}
