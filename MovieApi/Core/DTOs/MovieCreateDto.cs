using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace MovieApi.Core.DTOs
{
    public class MovieCreateDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name needs to at least include 3-30 chars!")]
        public string Title { get; set; } = string.Empty;
        [Required]
        public DateTime Year { get; set; }
        [Required]
        [Range(60, 250, ErrorMessage = "The duration on the movie can only be between 60-250!")]
        public int Duration { get; set; }
        [Required]
        [Range(0, 9, ErrorMessage = "Please select a valid genre!")]
        public int GenreId { get; set; }
        public string? Synopsis { get; set; }
        public string? Language { get; set; }
        [Required]
        public int Budget { get; set; }
    }
}
