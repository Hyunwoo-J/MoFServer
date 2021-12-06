using System;
using MoFModel.Entities;

namespace MoFModel.Models
{
    /// <summary>
    /// 리뷰 POST 응답 모델
    /// </summary>
    public class ReviewPostResponse : CommonResponse
    {
        /// <summary>
        /// 클라이언트에서 보내는 리뷰 모델
        /// </summary>
        public Review Review { get; set; }
    }
}
