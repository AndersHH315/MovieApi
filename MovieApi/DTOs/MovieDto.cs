using MovieApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class MovieDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name needs to at least include 3-30 chars!")]
        public string? Title { get; set; }
        [Required]
        public DateTime Year { get; set; }
        [Required]
        [Range(60, 250, ErrorMessage = "The price range needs to be between 60-250!")]
        public int Duration { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name needs to at least include 5-10 chars!")]
        public string? Genre { get; set; }

    }
}
