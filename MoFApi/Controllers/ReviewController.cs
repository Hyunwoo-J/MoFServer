using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoFModel.Contexts;
using MoFModel.Entities;
using MoFModel.Models;
using MoFModel.Services;

namespace MoFApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 전체 리뷰 데이터를 가져옵니다.
        /// </summary>
        /// <returns> 서버 응답 코드와 메시지, 영화 목록 </returns>
        [HttpGet]
        public async Task<ActionResult<ReviewListResponse>> GetReview()
        {
            var list = await _context.Review
                .Include(r => r.MovieTheater)
                .OrderBy(r => r.ReviewId)
                .Select(r => new ReviewDto(r))
                .ToListAsync();

            return Ok(new ReviewListResponse
            {
                Code = ResultCode.Ok,
                TotalCount = list.Count,
                List = list
            });
        }

        /// <summary>
        /// 검색한 id와 일치하는 리뷰 데이터를 가져옵니다.
        /// </summary>
        /// <param name="id"> 검색할 id </param>
        /// <returns> 검색된 리뷰 데이터 </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponse>> GetReview(int id)
        {
            var review = await _context.Review
                .Include(r => r.MovieTheater)
                .Where(r => r.ReviewId == id)
                .Select(r => new ReviewDto(r))
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.ReviewNotExists,
                    Message = "Review not exists"
                });
            }

            return Ok(new ReviewResponse
            {
                Code = ResultCode.Ok,
                Review = review
            });
        }

        /// <summary>
        /// 저장된 리뷰 데이터를 수정합니다.
        /// </summary>
        /// <param name="id"> 수정할 리뷰 아이디 </param>
        /// <param name="review"> 새로 넣을 리뷰 </param>
        /// <returns> 서버 응답 코드와 메시지 </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewPutData review)
        {
            if (id != review.ReviewId)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.ReviewNotExists,
                    Message = "Not exist"
                });
            }

            var targetReview = await _context.Review
                .Where(r => r.ReviewId == id)
                .FirstOrDefaultAsync();

            var movieTheater = _context.MovieTheater
                .Where(m => m.Name == review.MovieTheater)
                .FirstOrDefault();

            targetReview.MovieTitle = review.MovieTitle;
            targetReview.PosterPath = review.PosterPath;
            targetReview.BackdropPath = review.BackdropPath;
            targetReview.ReleaseDate = review.ReleaseDate;
            targetReview.StarPoint = review.StarPoint;
            targetReview.ViewingDate = review.ViewingDate;
            targetReview.Person = review.Person;
            targetReview.Memo = review.Memo;
            targetReview.UpdateDate = review.UpdateDate;
            targetReview.MovieTheater = movieTheater;

            await _context.SaveChangesAsync();

            return Ok(new ReviewPutResponse
            {
                Code = ResultCode.Ok,
            });
        }

        /// <summary>
        /// 리뷰를 저장합니다.
        /// </summary>
        /// <param name="review"> 전달된 리뷰 데이터 </param>
        /// <returns> 리뷰 객체 </returns>
        [HttpPost]
        public async Task<ActionResult<Review>> PostMovieTheater(ReviewPostData review)
        {
            var existingMovieTheater = _context.MovieTheater
                .Where(m => m.Name == review.MovieTheater)
                .FirstOrDefault();

            if (existingMovieTheater == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail
                });
            };

            var existingReview = await _context.Review
                .Where(r => r.MovieTitle == review.MovieTitle)
                .FirstOrDefaultAsync();

            if (existingReview != null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "This review has already been written."
                });
            }

            var newReview = new Review
            {
                MovieId = review.MovieId,
                MovieTitle = review.MovieTitle,
                PosterPath = review.PosterPath,
                BackdropPath = review.BackdropPath,
                ReleaseDate = review.ReleaseDate,
                StarPoint = review.StarPoint,
                ViewingDate = review.ViewingDate,
                Person = review.Person,
                Memo = review.Memo,
                UpdateDate = review.UpdateDate,
                MovieTheaterId = existingMovieTheater.MovieTheaterId
            };

            _context.Review.Add(newReview);
            await _context.SaveChangesAsync();

            return Ok(new CommonResponse
            {
                Code = ResultCode.Ok
            });
        }

        /// <summary>
        /// 리뷰를 삭제합니다.
        /// </summary>
        /// <param name="id"> 리뷰 아이디 </param>
        /// <returns> 서버 응답 코드 </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "존재하지 않는 리뷰입니다."
                });
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(new CommonResponse
            {
                Code = ResultCode.Ok,
            });
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.ReviewId == id);
        }
    }
}