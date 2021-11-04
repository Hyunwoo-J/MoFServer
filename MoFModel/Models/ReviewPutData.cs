using System;
namespace MoFModel.Models
{
    /// <summary>
    /// 리뷰 PUT 모델
    /// </summary>
    public class ReviewPutData : ReviewPostData
    {
        /// <summary>
        /// 리뷰 ID
        /// </summary>
        public int ReviewId { get; set; }
    }
}
