using Microsoft.AspNetCore.Mvc;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Interfaces
{
    public interface IMovieService
    {
        Task<MovieDto?> GetMovieByIdAsync(int id);
        Task<IEnumerable<MovieDto?>?> GetAllMoviesAsync();
        Task<MovieDetailDto?> GetMovieDetailsAsync(int id);
        Task<Movie?> PutMovieAsync(int id, MovieUpdateDto movieDto);
        Task<Movie> PostMovieAsync(MovieDto movieDto);
        Task<Movie?> DeleteMovieAsync(int id);
    }
}
