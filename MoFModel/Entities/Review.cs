﻿using System;
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
        /// 리뷰 ID
        /// </summary>
        public int ReviewId { get; set; }

        /// <summary>
        /// 유저 ID
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// 영화 ID
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 영화 제목
        /// </summary>
        [Required]
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
        /// 영화관 ID
        /// </summary>
        public int? MovieTheaterId { get; set; }

        /// <summary>
        /// 영화관 데이터
        /// </summary>
        [JsonIgnore]
        public MovieTheater MovieTheater { get; set; }

        /// <summary>
        /// 같이 본 친구
        /// </summary>
        [Required]
        public string Person { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 작성 날짜
        /// </summary>
        public DateTime InsertDate = DateTime.UtcNow;

        /// <summary>
        /// 업데이트 날짜
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
