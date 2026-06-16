namespace MovieApi.Models;

public class MovieDetails
{
    public int Id { get; set; }
    public string? Synopsis { get; set; }
    public string? Language { get; set; }
    public int Budget { get; set; }

    public int MovieId { get; set; }
}
