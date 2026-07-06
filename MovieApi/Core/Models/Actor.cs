using System.ComponentModel.DataAnnotations;

namespace MovieApi.Core.Models;

public class Actor
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime BirthYear { get; set; }

    public ICollection<Movie> Movies { get; set; } = [];
}
