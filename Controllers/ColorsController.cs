using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tms.Backend.Entities;

namespace Tms.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ColorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

        public ColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/colors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
            return await _context.Colors.ToListAsync();
        }

        // GET: api/colors/{colorId}
        [HttpGet("{colorId}")]
        public async Task<ActionResult<Color>> GetColor(int colorId)
        {
            var color = await _context.Colors.FindAsync(colorId);

            if (color == null)
            {
                return NotFound();
            }

            return color;
        }

        // POST: api/colors
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(Color color)
        {
            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColor", new { colorId = color.Id }, color);
        }

        // PUT: api/colors/{colorId}
        [HttpPut("{colorId}")]
        public async Task<IActionResult> PutColor(int colorId, Color color)
        {
            if (colorId != color.Id)
            {
                return BadRequest();
            }

            _context.Entry(color).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(colorId))
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

        // DELETE: api/colors/{colorId}
        [HttpDelete("{colorId}")]
        public async Task<IActionResult> DeleteColor(int colorId)
        {
            var color = await _context.Colors.FindAsync(colorId);
            if (color == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorExists(int colorId)
        {
            return _context.Colors.Any(c => c.Id == colorId);
        }
}