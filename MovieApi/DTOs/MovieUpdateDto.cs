using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class MovieUpdateDto
    {
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name needs to at least include 3-30 chars!")]
        public string? Title { get; set; }
        public DateTime Year { get; set; }
        [Range(60, 250, ErrorMessage = "The price range needs to be between 60-250!")]
        public int Duration { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name needs to at least include 5-10 chars!")]
        public string? Genre { get; set; }
    }
}
