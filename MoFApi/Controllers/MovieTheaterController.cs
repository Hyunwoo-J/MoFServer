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

namespace MoFApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieTheaterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieTheaterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MovieTheaterApi
        [HttpGet]
        public async Task<ActionResult<MovieTheaterListResponse>> GetMovieTheater()
        {
            var list = await _context.MovieTheater
                .OrderBy(m => m.MovieTheaterId)
                .ToListAsync();

            return Ok(new MovieTheaterListResponse
            {
                Code = ResultCode.Ok,
                List = list
            });
        }

        // GET: api/MovieTheaterApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieTheater>> GetMovieTheater(int id)
        {
            var movieTheater = await _context.MovieTheater.FindAsync(id);

            if (movieTheater == null)
            {
                return NotFound();
            }

            return movieTheater;
        }

        // PUT: api/MovieTheaterApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovieTheater(int id, MovieTheater movieTheater)
        {
            if (id != movieTheater.MovieTheaterId)
            {
                return BadRequest();
            }

            _context.Entry(movieTheater).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieTheaterExists(id))
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

        // POST: api/MovieTheaterApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MovieTheater>> PostMovieTheater(MovieTheater movieTheater)
        {
            var existingMovieTheater = await _context.MovieTheater
                .Where(m => m.Name == movieTheater.Name)
                .FirstOrDefaultAsync();

            if (existingMovieTheater == null)
            {
                _context.MovieTheater.Add(movieTheater);
                await _context.SaveChangesAsync();
            }

            return Ok(new CommonResponse
            {
                Code = ResultCode.Ok
            });
        }

        // DELETE: api/MovieTheaterApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MovieTheater>> DeleteMovieTheater(int id)
        {
            var movieTheater = await _context.MovieTheater.FindAsync(id);
            if (movieTheater == null)
            {
                return NotFound();
            }

            _context.MovieTheater.Remove(movieTheater);
            await _context.SaveChangesAsync();

            return movieTheater;
        }

        private bool MovieTheaterExists(int id)
        {
            return _context.MovieTheater.Any(e => e.MovieTheaterId == id);
        }
    }
}
