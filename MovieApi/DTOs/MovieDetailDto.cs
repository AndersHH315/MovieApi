using MovieApi.Models;

namespace MovieApi.DTOs
{
    public class MovieDetailDto
    {
        public string? Synopsis { get; set; }
        public string? Language { get; set; }
        public int Budget { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Actor>? Actors { get; set; } = new List<Actor>();
    }
}
