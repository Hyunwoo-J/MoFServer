using System;
using System.Collections.Generic;
using MoFModel.Entities;

namespace MoFModel.Models
{
    /// <summary>
    /// 서버 응답 정보
    /// </summary>
    public class CommonResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// 리뷰 POST
    /// </summary>
    public class ReviewPostResponse: CommonResponse
    {
        public Review Review { get; set; }
    }

    /// <summary>
    /// 영화관 목록 응답
    /// </summary>
    public class MovieTheaterListResponse: CommonResponse
    {
        public int TotalCount { get; set; }
        public List<MovieTheater> List { get; set; }
    }

    /// <summary>
    /// 리뷰 목록 응답
    /// </summary>
    public class ReviewListResponse: CommonResponse
    {
        public int TotalCount { get; set; }
        public List<ReviewDto> List { get; set; }
    }

    /// <summary>
    /// 리뷰 PUT
    /// </summary>
    public class ReviewPutResponse: ReviewPostResponse
    {

    }
}
