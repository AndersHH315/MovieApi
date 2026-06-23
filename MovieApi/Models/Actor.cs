using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models;

public class Actor
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
    public DateTime BirthYear { get; set; }

    public ICollection<Movie> Movies { get; set; } = [];
}
