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
    public class ProductStatusController : ControllerBase
    {
        private readonly OrderProjectContext _context;

        public ProductStatusController(OrderProjectContext context)
        {
            _context = context;
        }

        // GET: api/ProductStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> GetProductStatus()
        {
            return await _context.ProductStatus.ToListAsync();
        }

        // GET: api/ProductStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStatus>> GetProductStatus(int id)
        {
            var productStatus = await _context.ProductStatus.FindAsync(id);

            if (productStatus == null)
            {
                return NotFound();
            }

            return productStatus;
        }

        // PUT: api/ProductStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductStatus(int id, ProductStatus productStatus)
        {
            if (id != productStatus.StatusId)
            {
                return BadRequest();
            }

            _context.Entry(productStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStatusExists(id))
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

        // POST: api/ProductStatus
        [HttpPost]
        public async Task<ActionResult<ProductStatus>> PostProductStatus(ProductStatus productStatus)
        {
            _context.ProductStatus.Add(productStatus);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductStatusExists(productStatus.StatusId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductStatus", new { id = productStatus.StatusId }, productStatus);
        }

        // DELETE: api/ProductStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductStatus>> DeleteProductStatus(int id)
        {
            var productStatus = await _context.ProductStatus.FindAsync(id);
            if (productStatus == null)
            {
                return NotFound();
            }

            _context.ProductStatus.Remove(productStatus);
            await _context.SaveChangesAsync();

            return productStatus;
        }

        private bool ProductStatusExists(int id)
        {
            return _context.ProductStatus.Any(e => e.StatusId == id);
        }
    }
}
