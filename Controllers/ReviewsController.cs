using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tms.Backend.Entities;

namespace Tms.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReviewsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/reviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
    {
        return await _context.Reviews.ToListAsync();
    }

    // GET: api/reviews/{reviewId}
    [HttpGet("{reviewId}")]
    public async Task<ActionResult<Review>> GetReview(int reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);

        if (review == null)
        {
            return NotFound();
        }

        return review;
    }

    // POST: api/reviews
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetReview", new { reviewId = review.Id }, review);
    }

    // PUT: api/reviews/{reviewId}
    [HttpPut("{reviewId}")]
    public async Task<IActionResult> PutReview(int reviewId, Review review)
    {
        if (reviewId != review.Id)
        {
            return BadRequest();
        }

        _context.Entry(review).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ReviewExists(reviewId))
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

    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(int reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);
        if (review == null)
        {
            return NotFound();
        }
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    private bool ReviewExists(int reviewId)
    {
        return _context.Reviews.Any(r => r.Id == reviewId);
    }
}