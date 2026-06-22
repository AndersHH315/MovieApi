using MovieApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public class MovieDetailDto
    {

        public string? Title { get; set; }
        public DateTime Year { get; set; }
        public int Duration { get; set; }
        public string? Genre { get; set; }
        public string? Synopsis { get; set; }
        public string? Language { get; set; }
        public int Budget { get; set; }

        public ICollection<ReviewDto> Reviews { get; set; } = [];
        public ICollection<ActorDto> Actors { get; set; } = [];

    }
}
