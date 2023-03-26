using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tms.Backend.Entities;

namespace Tms.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductImagesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductImagesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/productimages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Image>>> GetProductImages()
    {
        return await _context.Images.ToListAsync();
    }

    // GET: api/productimages/{productImageId}
    [HttpGet("{productImageId}")]
    public async Task<ActionResult<Image>> GetProductImage(int productImageId)
    {
        var productImage = await _context.Images.FindAsync(productImageId);

        if (productImage == null)
        {
            return NotFound();
        }

        return productImage;
    }
    
    // POST: api/productimages
    [HttpPost]
    public async Task<ActionResult<Image>> PostProductImage(Image productImage)
    {
        _context.Images.Add(productImage);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProductImage", new { productImageId = productImage.Id }, productImage);
    }

    // PUT: api/productimages/{productImageId}
    [HttpPut("{productImageId}")]
    public async Task<IActionResult> PutProductImage(int productImageId, Image productImage)
    {
        if (productImageId != productImage.Id)
        {
            return BadRequest();
        }

        _context.Entry(productImage).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductImageExists(productImageId))
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

    // DELETE: api/productimages/{productImageId}
    [HttpDelete("{productImageId}")]
    public async Task<IActionResult> DeleteProductImage(int productImageId)
    {
        var productImage = await _context.Images.FindAsync(productImageId);
        if (productImage == null)
        {
            return NotFound();
        }

        _context.Images.Remove(productImage);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductImageExists(int productImageId)
    {
        return _context.Images.Any(pi => pi.Id == productImageId);
    }
}