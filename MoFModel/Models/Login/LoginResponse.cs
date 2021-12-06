using System;
namespace MoFModel.Models
{
    /// <summary>
    /// 로그인 응답 모델
    /// </summary>
    public class LoginResponse : CommonResponse
    {
        /// <summary>
        /// 유저 ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// JWT 토큰
        /// </summary>
        public string Token { get; set; }
    }
}
