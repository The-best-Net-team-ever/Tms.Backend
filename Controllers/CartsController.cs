using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tms.Backend.Entities;

namespace Tms.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CartsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: api/carts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
    {
        return await _context.Carts
            .Include(c => c.ProductVariations)
            .ToListAsync();
    }

    // GET: api/carts/{cartId}
    [HttpGet("{cartId}")]
    public async Task<ActionResult<Cart>> GetCart(int cartId)
    {
        var cart = await _context.Carts
            .Include(x => x.ProductVariations)
            .SingleOrDefaultAsync(c => c.Id == cartId);

        if (cart == null)
        {
            return NotFound();
        }

        return cart;
    }
    
    // POST: api/carts
    [HttpPost]
    public async Task<ActionResult<Cart>> PostCart(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCart", new { cartId = cart.Id }, cart);
    }
    
    // POST: api/carts/{id}/products
    [HttpPost("{cartId}/products")]
    public async Task<ActionResult<Cart>> AddProductInCart(int cartId, ProductVariation productVariation)
    {
        var cart = await _context.Carts.FindAsync(cartId);
        if (cart is null)
        {
            return NotFound($"The cart with id {cartId} was not found.");
        }
        
        cart.ProductVariations.Add(productVariation);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction("GetCart", new { cartId = cart.Id }, cart);
    }
    
    // DELETE: api/carts/{cartId}
    [HttpDelete("{cartId}")]
    public async Task<IActionResult> DeleteCart(int cartId)
    {
        var cart = await _context.Carts.FindAsync(cartId);
        if (cart == null)
        {
            return NotFound();
        }

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}