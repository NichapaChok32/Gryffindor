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
    public class PurchaseRequestsController : ControllerBase
    {
        private readonly OrderProjectContext _context;

        public PurchaseRequestsController(OrderProjectContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseRequests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseRequest>>> GetPurchaseRequest()
        {
            return await _context.PurchaseRequest.ToListAsync();
        }

        // GET: api/PurchaseRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseRequest>> GetPurchaseRequest(int id)
        {
            var purchaseRequest = await _context.PurchaseRequest.FindAsync(id);

            if (purchaseRequest == null)
            {
                return NotFound();
            }

            return purchaseRequest;
        }

        // PUT: api/PurchaseRequests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseRequest(int id, PurchaseRequest purchaseRequest)
        {
            if (id != purchaseRequest.PurchaseId)
            {
                return BadRequest();
            }

            _context.Entry(purchaseRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseRequestExists(id))
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

        // POST: api/PurchaseRequests
        [HttpPost]
        public async Task<ActionResult<PurchaseRequest>> PostPurchaseRequest(PurchaseRequest purchaseRequest)
        {
            _context.PurchaseRequest.Add(purchaseRequest);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PurchaseRequestExists(purchaseRequest.PurchaseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPurchaseRequest", new { id = purchaseRequest.PurchaseId }, purchaseRequest);
        }

        // DELETE: api/PurchaseRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchaseRequest>> DeletePurchaseRequest(int id)
        {
            var purchaseRequest = await _context.PurchaseRequest.FindAsync(id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }

            _context.PurchaseRequest.Remove(purchaseRequest);
            await _context.SaveChangesAsync();

            return purchaseRequest;
        }

        private bool PurchaseRequestExists(int id)
        {
            return _context.PurchaseRequest.Any(e => e.PurchaseId == id);
        }
    }
}
