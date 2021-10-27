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
    /// 리뷰 Post
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

    public class ReviewResponse: CommonResponse
    {
        public ReviewDto Review { get; set; }
    }

    /// <summary>
    /// 리뷰 Put
    /// </summary>
    public class ReviewPutResponse: CommonResponse
    {

    }

    public class EmailLoginPostData
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class EmailJoinPostData: EmailLoginPostData
    {

    }

    public class LoginResponse: CommonResponse
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }

    public class JoinResponse: LoginResponse
    {

    }
}
