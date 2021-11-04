using System;
using MoFModel.Entities;

namespace MoFModel.Models
{
    /// <summary>
    /// 리뷰 DTO 모델
    /// </summary>
    public class ReviewDto
    {
        public ReviewDto(Review review)
        {
            ReviewId = review.ReviewId;
            MovieId = review.MovieId;
            MovieTitle = review.MovieTitle;
            PosterPath = review.PosterPath;
            BackdropPath = review.BackdropPath;
            ReleaseDate = review.ReleaseDate;
            StarPoint = review.StarPoint;
            ViewingDate = review.ViewingDate;
            MovieTheater = review.MovieTheater.Name;
            Person = review.Person;
            Memo = review.Memo;
            UpdateDate = review.UpdateDate;
        }

        /// <summary>
        /// 리뷰 ID
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// 영화 ID
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 영화 제목
        /// </summary>
        public string MovieTitle { get; set; }

        /// <summary>
        /// 배경 이미지 경로
        /// </summary>
        public string PosterPath { get; set; }

        /// <summary>
        /// 배경 이미지 경로
        /// </summary>
        public string BackdropPath { get; set; }

        /// <summary>
        /// 개봉일
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// 별점
        /// </summary>
        public double StarPoint { get; set; }

        /// <summary>
        /// 영화 본 날짜
        /// </summary>
        public DateTime ViewingDate { get; set; }

        /// <summary>
        /// 영화관 데이터
        /// </summary>
        public string MovieTheater { get; set; }

        /// <summary>
        /// 같이 본 친구
        /// </summary>
        public string Person { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 업데이트 날짜
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
