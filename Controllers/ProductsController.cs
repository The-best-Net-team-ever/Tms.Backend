using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tms.Backend.Entities;

namespace Tms.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/products/{productId}
        [HttpGet("{productId}")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { productId = product.Id }, product);
        }

        // PUT: api/products/{productId}
        [HttpPut("{productId}")]
        public async Task<IActionResult> PutProduct(int productId, Product product)
        {
            if (productId != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(productId))
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

        // DELETE: api/products/{productId}
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int productId)
        {
            return _context.Products.Any(p => p.Id == productId);
        }
}