using System;
namespace MoFModel.Models
{
    /// <summary>
    /// 리뷰 POST 모델
    /// </summary>
    public class ReviewPostData
    {
        /// <summary>
        /// 영화 ID
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// 영화 제목
        /// </summary>
        public string MovieTitle { get; set; }

        /// <summary>
        /// 영화 포스터 경로
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
        /// 같이 본 친구
        /// </summary>
        public string Person { get; set; }

        /// <summary>
        /// 메모
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 영화관
        /// </summary>
        public string MovieTheater { get; set; }
    }
}
