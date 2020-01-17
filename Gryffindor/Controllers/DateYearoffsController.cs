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
    public class DateYearoffsController : ControllerBase
    {
        private readonly OrderProjectContext _context;

        public DateYearoffsController(OrderProjectContext context)
        {
            _context = context;
        }

        // GET: api/DateYearoffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DateYearoff>>> GetDateYearoff()
        {
            return await _context.DateYearoff.ToListAsync();
        }

        // GET: api/DateYearoffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DateYearoff>> GetDateYearoff(int id)
        {
            var dateYearoff = await _context.DateYearoff.FindAsync(id);

            if (dateYearoff == null)
            {
                return NotFound();
            }

            return dateYearoff;
        }

        // PUT: api/DateYearoffs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDateYearoff(int id, DateYearoff dateYearoff)
        {
            if (id != dateYearoff.DateId)
            {
                return BadRequest();
            }

            _context.Entry(dateYearoff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateYearoffExists(id))
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

        // POST: api/DateYearoffs
        [HttpPost]
        public async Task<ActionResult<DateYearoff>> PostDateYearoff(DateYearoff dateYearoff)
        {
            _context.DateYearoff.Add(dateYearoff);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DateYearoffExists(dateYearoff.DateId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDateYearoff", new { id = dateYearoff.DateId }, dateYearoff);
        }

        // DELETE: api/DateYearoffs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DateYearoff>> DeleteDateYearoff(int id)
        {
            var dateYearoff = await _context.DateYearoff.FindAsync(id);
            if (dateYearoff == null)
            {
                return NotFound();
            }

            _context.DateYearoff.Remove(dateYearoff);
            await _context.SaveChangesAsync();

            return dateYearoff;
        }

        private bool DateYearoffExists(int id)
        {
            return _context.DateYearoff.Any(e => e.DateId == id);
        }
    }
}
