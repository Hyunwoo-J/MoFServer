using System;
namespace MoFModel.Models
{
    /// <summary>
    /// SNS 로그인 POST 모델
    /// </summary>
    public class SocialLoginPostData
    {
        /// <summary>
        /// 공급자
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// 아이디
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 이메일
        /// </summary>
        public string Email { get; set; }
    }
}
