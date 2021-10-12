using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoFModel.Contexts;
using MoFModel.Entities;
using MoFModel.Models;

namespace MovieTheaterInfomation.Controllers
{
    [Route("bi/review")]
    [ApiController]
    public class ReviewApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: bi/Review
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Review>> PostMovieTheater(Review review)
        {
            var existingReview = await _context.Review
                .Where(r => r.MovieTitle == review.MovieTitle)
                .FirstOrDefaultAsync();

            if (existingReview == null)
            {
                _context.Review.Add(review);
                await _context.SaveChangesAsync();
            }

            return Ok(new CommonResponse
            {
                ResultCode = ResultCode.Ok
            });
        }
    }
}
