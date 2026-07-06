using System.ComponentModel.DataAnnotations;

namespace MovieApi.Core.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime Year { get; set; }
    public int Duration { get; set; }

    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;

    public MovieDetails MovieDetails { get; set; } = null!;

    public ICollection<Review> Reviews { get; set; } = [];

    public ICollection<Actor> Actors { get; set; } = [];
}
