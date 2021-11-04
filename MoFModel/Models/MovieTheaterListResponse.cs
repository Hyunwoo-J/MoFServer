using System;
using System.Collections.Generic;
using MoFModel.Entities;

namespace MoFModel.Models
{
    /// <summary>
    /// 영화관 목록 응답 모델
    /// </summary>
    public class MovieTheaterListResponse : CommonResponse
    {
        public int TotalCount { get; set; }
        public List<MovieTheater> List { get; set; }
    }
}
