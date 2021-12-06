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
    [Route("theater")]
    [ApiController]
    public class MovieTheaterApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieTheaterApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 영화관 목록을 Bulk Insert 합니다.
        /// </summary>
        /// <param name="movieTheater"> 영화관 데이터 </param>
        /// <returns> 서버 응답 코드 </returns>
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
    }
}
