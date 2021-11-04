using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoFModel.Entities
{
    /// <summary>
    /// 영화관 모델
    /// </summary>
    public class MovieTheater
    {
        /// <summary>
        /// 영화관 ID
        /// </summary>
        [Display(Name = "ID")]
        public int MovieTheaterId { get; set; }

        /// <summary>
        /// 광역단체
        /// </summary>
        [Required]
        [Display(Name = "광역단체")]
        public string MetropolitanCouncil { get; set; }

        /// <summary>
        /// 기초단체
        /// </summary>
        [Required]
        [Display(Name = "기초단체")]
        public string BasicOrganization { get; set; }

        /// <summary>
        /// 영화상영관명
        /// </summary>
        [Required]
        [Display(Name = "영화상영관명")]
        public string Name { get; set; }

        /// <summary>
        /// 리뷰 목록
        /// </summary>
        public List<Review> Reviews { get; set; }
    }
}
