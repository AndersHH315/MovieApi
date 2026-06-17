using MovieApi.Models;

namespace MovieApi.DTOs
{
    public class MovieDto
    {
        public string? Title { get; set; }
        public DateTime Year { get; set; }
        public int Duration { get; set; }
        public string? Genre { get; set; }

    }
}
