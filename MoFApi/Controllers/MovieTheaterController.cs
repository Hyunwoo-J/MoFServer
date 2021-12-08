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

        /// <summary>
        /// 영화관 목록을 불러옵니다.
        /// </summary>
        /// <returns> 영화관 목록 응답 객체 </returns>
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

        /// <summary>
        /// 아이디에 해당하는 영화관 정보를 가져옵니다.
        /// </summary>
        /// <param name="id"> 영화관 아이디 </param>
        /// <returns> 일치하는 영화관 정보 </returns>
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

        /// <summary>
        /// 아이디에 해당하는 영화관 정보를 수정합니다.
        /// </summary>
        /// <param name="id"> 영화관 아이디 </param>
        /// <param name="movieTheater"> 수정된 영화관 정보 </param>
        /// <returns> 서버 응답 코드 </returns>
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

        /// <summary>
        /// 영화관 정보를 저장합니다.
        /// </summary>
        /// <param name="movieTheater"> 저장할 영화관 정보 </param>
        /// <returns> 서버 응답 코드와 메시지 </returns>
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

                return Ok(new CommonResponse
                {
                    Code = ResultCode.Ok,
                    Message = "Movie theater has been saved"
                });
            }

            return Ok(new CommonResponse
            {
                Code = ResultCode.TheaterExists,
                Message = "Movie theater already exists"
            });
        }

        /// <summary>
        /// 아이디에 해당하는 영화관 정보를 삭제합니다.
        /// </summary>
        /// <param name="id"> 영화관 아이디 </param>
        /// <returns> 삭제된 영화관 정보 </returns>
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
