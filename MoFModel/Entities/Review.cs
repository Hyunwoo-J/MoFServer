using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MoFModel.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        public string PosterPath { get; set; }

        public string BackdropPath { get; set; }

        public DateTime RealeaseDate { get; set; }

        public double StarPoint { get; set; }

        [Required]
        public DateTime ViewingDate { get; set; }

        public int? MovieTheaterId { get; set; }

        [JsonIgnore]
        public MovieTheater MovieTheater { get; set; }

        [Required]
        public string Person { get; set; }

        public string Memo { get; set; }

        public DateTime InsertDate = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; }
    }
}
