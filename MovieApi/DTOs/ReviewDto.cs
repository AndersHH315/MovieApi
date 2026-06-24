using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class ReviewDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name needs to at least include 3-20 chars!")]
        public string? ReviewerName { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 0, ErrorMessage = "Name needs to at least include 0-150 chars!")]
        public string? Comment { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "The rating on the movie can only be between 1-5!")]
        public int Rating { get; set; }
    }
}
