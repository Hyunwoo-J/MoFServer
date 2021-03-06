using System;
using System.Collections.Generic;
using MoFModel.Entities;

namespace MoFModel.Models
{
    /// <summary>
    /// 기본 서버 응답 모델
    /// </summary>
    public class CommonResponse
    {
        /// <summary>
        /// 응답 코드
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 메시지
        /// </summary>
        public string Message { get; set; }
    }
}
