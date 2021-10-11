using System;
using System.Collections.Generic;
using MoFModel.Entities;

namespace MoFModel.Models
{
    public class MovieTheaterListResponse: CommonResponse
    {
        public int TotalCount { get; set; }
        public List<MovieTheater> List { get; set; }
    }
}
