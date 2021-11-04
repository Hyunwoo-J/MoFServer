using System;
using MoFModel.Entities;

namespace MoFModel.Models
{
    /// <summary>
    /// 리뷰 응답 모델
    /// </summary>
    public class ReviewResponse : CommonResponse
    {
        /// <summary>
        /// 리뷰 DTO
        /// </summary>
        public ReviewDto Review { get; set; }
    }
}
