﻿using System.ComponentModel.DataAnnotations.Schema;

namespace api_cinema_challenge.Models
{
    [Table("screenings")]
    public class Screening
    {
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey("Movie")]
        [Column("movieid")]
        public int MovieId { get; set; }
        [Column("screenid")]
        public int ScreenId { get; set; }
        [Column("capacity")]
        public int Capacity { get; set; }
        [Column("startsat")]
        public DateTime StartsAt { get; set; }
        [Column("createdat")]
        public DateTime CreatedAt { get; set; }
        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
        public Movie Movie { get; set; }
    }
}
