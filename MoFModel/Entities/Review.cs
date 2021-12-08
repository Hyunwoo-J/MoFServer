using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using MoFModel.Models;

namespace MoFModel.Entities
{
    /// <summary>
    /// 리뷰 모델
    /// </summary>
    public class Review
    {
        /// <summary>
        /// 리뷰 아이디
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// 유저 아이디
        /// </summary>
        [Required]
        [Display(Name = "유저 ID")]
        public string UserId { get; set; }

        /// <summary>
        /// 영화 아이디
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 영화 제목
        /// </summary>
        [Required]
        [Display(Name = "영화 제목")]
        public string MovieTitle { get; set; }

        /// <summary>
        /// 포스터 이미지 경로
        /// </summary>
        [Display(Name = "포스터 이미지 경로")]
        public string PosterPath { get; set; }

        /// <summary>
        /// 배경 이미지 경로
        /// </summary>
        [Display(Name = "배경 이미지 경로")]
        public string BackdropPath { get; set; }

        /// <summary>
        /// 개봉일
        /// </summary>
        [Display(Name = "개봉일")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// 별점
        /// </summary>
        [Display(Name = "평점")]
        public double StarPoint { get; set; }

        /// <summary>
        /// 영화 본 날짜
        /// </summary>
        [Display(Name = "영화 본 날짜")]
        public DateTime ViewingDate { get; set; }

        /// <summary>
        /// 영화관 아이디
        /// </summary>
        public int? MovieTheaterId { get; set; }

        /// <summary>
        /// 영화관 데이터
        /// </summary>
        [JsonIgnore]
        [Display(Name = "영화관")]
        public MovieTheater MovieTheater { get; set; }

        /// <summary>
        /// 같이 본 친구
        /// </summary>
        [Required]
        [Display(Name = "같이 본 친구")]
        public string Person { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        [Display(Name = "메모")]
        public string Memo { get; set; }

        /// <summary>
        /// 작성 날짜
        /// </summary>
        [Display(Name = "작성 날짜")]
        public DateTime InsertDate { get; set; }

        /// <summary>
        /// 업데이트 날짜
        /// </summary>
        [Display(Name = "업데이트 날짜")]
        public DateTime UpdateDate { get; set; }
    }
}
