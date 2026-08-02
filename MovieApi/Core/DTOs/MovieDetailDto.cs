using MovieApi.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Core.DTOs;

public class MovieDetailDto
{
    public string? Synopsis { get; set; }
    public string? Language { get; set; }
    public int Budget { get; set; }

}
