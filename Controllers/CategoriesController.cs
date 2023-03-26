using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tms.Backend.Entities;

namespace Tms.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/categories/{categoryId}
        [HttpGet("{categoryId}")]
        public async Task<ActionResult<Category>> GetCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // GET: api/categories/{categoryId}/subcategories
        [HttpGet("{categoryId}/subcategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetSubcategories(int categoryId)
        {
            var subcategories = await _context.Categories
                .Where(c => c.ParentCategory.Id == categoryId)
                .ToListAsync();

            return subcategories;
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { categoryId = category.Id }, category);
        }

        // PUT: api/categories/{categoryId}
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> PutCategory(int categoryId, Category category)
        {
            if (categoryId != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(categoryId))
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

        // DELETE: api/categories/{categoryId}
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }
}