using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoFModel.Entities
{
    public class MovieTheater
    {
        [Display(Name = "ID")]
        public int MovieTheaterId { get; set; }

        [Required]
        [Display(Name = "광역단체")]
        public string MetropolitanCouncil { get; set; }

        [Required]
        [Display(Name = "기초단체")]
        public string BasicOrganization { get; set; }

        [Required]
        [Display(Name = "영화상영관명")]
        public string Name { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
