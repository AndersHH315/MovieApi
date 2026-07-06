using Microsoft.AspNetCore.Mvc;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;

namespace MovieApi.Services.Contracts;

public interface IMovieService
{
    Task<MovieDto?> GetMovieByIdAsync(int id);
    Task<IEnumerable<MovieDto?>?> GetAllMoviesAsync();
    Task<MovieDetailDto?> GetMovieDetailsAsync(int id);
    Task<Movie?> PutMovieAsync(int id, MovieUpdateDto movieDto);
    Task<Movie> PostMovieAsync(MovieCreateDto movieCreateDto);
    Task<Movie?> DeleteMovieAsync(int id);
}
