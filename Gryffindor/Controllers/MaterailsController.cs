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
    public class MaterailsController : ControllerBase
    {
        private readonly OrderProjectContext _context;

        public MaterailsController(OrderProjectContext context)
        {
            _context = context;
        }

        // GET: api/Materails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materail>>> GetMaterail()
        {
            return await _context.Materail.ToListAsync();
        }

        // GET: api/Materails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materail>> GetMaterail(int id)
        {
            var materail = await _context.Materail.FindAsync(id);

            if (materail == null)
            {
                return NotFound();
            }

            return materail;
        }

        // PUT: api/Materails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterail(int id, Materail materail)
        {
            if (id != materail.MaterailId)
            {
                return BadRequest();
            }

            _context.Entry(materail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterailExists(id))
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

        // POST: api/Materails
        [HttpPost]
        public async Task<ActionResult<Materail>> PostMaterail(Materail materail)
        {
            _context.Materail.Add(materail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MaterailExists(materail.MaterailId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMaterail", new { id = materail.MaterailId }, materail);
        }

        // DELETE: api/Materails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Materail>> DeleteMaterail(int id)
        {
            var materail = await _context.Materail.FindAsync(id);
            if (materail == null)
            {
                return NotFound();
            }

            _context.Materail.Remove(materail);
            await _context.SaveChangesAsync();

            return materail;
        }

        private bool MaterailExists(int id)
        {
            return _context.Materail.Any(e => e.MaterailId == id);
        }
    }
}
