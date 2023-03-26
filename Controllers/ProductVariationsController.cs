using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tms.Backend.Entities;

namespace Tms.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductVariationsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductVariationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/productvariations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductVariation>>> GetProductVariations()
    {
        return await _context.ProductVariations.ToListAsync();
    }

    // GET: api/productvariations/{productVariationId}
    [HttpGet("{productVariationId}")]
    public async Task<ActionResult<ProductVariation>> GetProductVariation(int productVariationId)
    {
        var productVariation = await _context.ProductVariations.FindAsync(productVariationId);

        if (productVariation == null)
        {
            return NotFound();
        }

        return productVariation;
    }

    // POST: api/productvariations
        [HttpPost]
        public async Task<ActionResult<ProductVariation>> PostProductVariation(ProductVariation productVariation)
        {
            _context.ProductVariations.Add(productVariation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductVariation", new { productVariationId = productVariation.Id }, productVariation);
        }

        // PUT: api/productvariations/{productVariationId}
        [HttpPut("{productVariationId}")]
        public async Task<IActionResult> PutProductVariation(int productVariationId, ProductVariation productVariation)
        {
            if (productVariationId != productVariation.Id)
            {
                return BadRequest();
            }

            _context.Entry(productVariation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductVariationExists(productVariationId))
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

        // DELETE: api/productvariations/{productVariationId}
        [HttpDelete("{productVariationId}")]
        public async Task<IActionResult> DeleteProductVariation(int productVariationId)
        {
            var productVariation = await _context.ProductVariations.FindAsync(productVariationId);
            if (productVariation == null)
            {
                return NotFound();
            }

            _context.ProductVariations.Remove(productVariation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductVariationExists(int productVariationId)
        {
            return _context.ProductVariations.Any(pv => pv.Id == productVariationId);
        }
}