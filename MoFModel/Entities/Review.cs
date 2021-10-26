using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoFModel.Entities
{
    /// <summary>
    /// 리뷰 정보
    /// </summary>
    public class Review
    {
        public int ReviewId { get; set; }

        public int MovieId { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double StarPoint { get; set; }
        public DateTime ViewingDate { get; set; }

        public int? MovieTheaterId { get; set; }

        [JsonIgnore]
        public MovieTheater MovieTheater { get; set; }

        [Required]
        public string Person { get; set; }

        public string Memo { get; set; }

        public DateTime InsertDate = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; }
    }

    public class ReviewPostData
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double StarPoint { get; set; }
        public DateTime ViewingDate { get; set; }
        public string Person { get; set; }
        public string Memo { get; set; }
        public DateTime UpdateDate { get; set; }
        public string MovieTheater { get; set; }
    }

    public class ReviewPutData: ReviewPostData
    {
        public int ReviewId { get; set; }
    }

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

        public int ReviewId { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double StarPoint { get; set; }
        public DateTime ViewingDate { get; set; }
        public string MovieTheater { get; set; }
        public string Person { get; set; }
        public string Memo { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
