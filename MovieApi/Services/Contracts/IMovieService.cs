using Microsoft.AspNetCore.JsonPatch;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Core.Paging;

namespace MovieApi.Services.Contracts;

public interface IMovieService
{
    Task<MovieDto?> GetMovieByIdAsync(int id);
    Task<PagedResult<MovieDto>> GetAllMoviesAsync(PagingParameters paging);
    Task<AllMovieDetailsDto?> GetMovieDetailsAsync(int id);
    Task<Movie?> PutMovieAsync(int id, JsonPatchDocument<MovieUpdateDto> patchDoc);
    Task<Movie> PostMovieAsync(MovieCreateDto movieCreateDto);
    Task<Movie?> DeleteMovieAsync(int id);
}
