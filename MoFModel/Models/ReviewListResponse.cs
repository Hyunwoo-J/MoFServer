using System;
using System.Collections.Generic;
using MoFModel.Entities;

namespace MoFModel.Models
{
    /// <summary>
    /// 리뷰 목록 응답 모델
    /// </summary>
    public class ReviewListResponse : CommonResponse
    {
        /// <summary>
        /// 총 리뷰 수
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 리뷰 목록
        /// </summary>
        public List<ReviewDto> List { get; set; }
    }
}
