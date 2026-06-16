namespace MovieApi.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateTime Year { get; set; }
    public int Duration { get; set; }

    public int GenreId { get; set; }
    public Genre? Genres { get; set; }

    public MovieDetails? MovieDetails { get; set; }

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    public ICollection<Actor>? Actors { get; set; }
}
