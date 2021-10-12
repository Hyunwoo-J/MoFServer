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
    [Route("bi/theater")]
    [ApiController]
    public class MovieTheaterApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieTheaterApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: bi/theater
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
                ResultCode = ResultCode.Ok
            });
        }
    }
}
