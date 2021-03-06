using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IConfiguration Configuration { get; }

        public ReviewController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            Configuration = configuration;
            _context = context;
        }

        /// <summary>
        /// 유저 아이디와 일치하는 전체 리뷰 데이터를 가져옵니다.
        /// </summary>
        /// <returns> 리뷰 목록 응답 객체 </returns>
        [HttpGet]
        public async Task<ActionResult<ReviewListResponse>> GetReview()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "User not found"
                });
            }

            var list = await _context.Review
                .Where(r => r.UserId == user.Id)
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
        /// <param name="id"> 검색할 리뷰 아이디 </param>
        /// <returns> 리뷰 응답 객체 </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponse>> GetReview(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "User not found"
                });
            }

            var review = await _context.Review
                .Include(r => r.MovieTheater)
                .Where(r => r.ReviewId == id && r.UserId == user.Id)
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
        /// <param name="review"> 새로 넣을 리뷰 데이터 </param>
        /// <returns> 리뷰 수정 응답 객체 </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewPutData review)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "User not found"
                });
            }

            if (id != review.ReviewId)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "Review id not exist"
                });
            }

            var targetReview = await _context.Review
                .Where(r => r.ReviewId == id && r.UserId == user.Id)
                .FirstOrDefaultAsync();
            if (targetReview == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "Review not found"
                });
            }

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
            targetReview.MovieTheater = movieTheater;
            targetReview.UpdateDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new ReviewPutResponse
            {
                Code = ResultCode.Ok,
                Message = "Review has been saved"
            });
        }

        /// <summary>
        /// 리뷰를 저장합니다.
        /// </summary>
        /// <param name="review"> 저장할 리뷰 데이터 </param>
        /// <returns> 서버 응답 코드와 메시지 </returns>
        [HttpPost]
        public async Task<ActionResult<Review>> PostMovieTheater(ReviewPostData review)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "User not found"
                });
            }

            var existingMovieTheater = _context.MovieTheater
                .Where(m => m.Name == review.MovieTheater)
                .FirstOrDefault();

            if (existingMovieTheater == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "Movie theater not found"
                });
            };

            var existingReview = await _context.Review
                .Where(r => r.MovieTitle == review.MovieTitle && r.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (existingReview != null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "This review has already been written"
                });
            }

            var newReview = new Review
            {
                UserId = user.Id,
                MovieId = review.MovieId,
                MovieTitle = review.MovieTitle,
                PosterPath = review.PosterPath,
                BackdropPath = review.BackdropPath,
                ReleaseDate = review.ReleaseDate,
                StarPoint = review.StarPoint,
                ViewingDate = review.ViewingDate,
                Person = review.Person,
                Memo = review.Memo,
                MovieTheaterId = existingMovieTheater.MovieTheaterId,
                InsertDate = DateTime.UtcNow
            };

            _context.Review.Add(newReview);
            await _context.SaveChangesAsync();

            return Ok(new CommonResponse
            {
                Code = ResultCode.Ok,
                Message = "Review has been saved"
            });
        }

        /// <summary>
        /// 리뷰를 삭제합니다.
        /// </summary>
        /// <param name="id"> 삭제할 리뷰 아이디 </param>
        /// <returns> 서버 응답 코드와 메시지 </returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "User not found"
                });
            }

            var review = await _context.Review
                .Where(r => r.ReviewId == id && r.UserId == user.Id)
                .FirstOrDefaultAsync();
            if (review == null)
            {
                return Ok(new CommonResponse
                {
                    Code = ResultCode.Fail,
                    Message = "Review not exist"
                });
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(new CommonResponse
            {
                Code = ResultCode.Ok,
                Message = "Review has been deleted"
            });
        }
    }
}