using Microsoft.AspNetCore.JsonPatch;
using MovieApi.Core.DomainContracts;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Core.Paging;
using MovieApi.Services.Contracts;

namespace MovieApi.Services;

public class MovieService(IUnitOfWork unit) : IMovieService
{
    private readonly IUnitOfWork _unit = unit;

    public async Task<PagedResult<MovieDto>> GetAllMoviesAsync(PagingParameters paging)
    {
        var movies = await _unit.Movies.GetAllAsync();
        return await movies.Select(m => new MovieDto
        {
            Id = m.Id,
            Title = m.Title,
            Year = m.Year,
            Duration = m.Duration,
            Genre = m.Genre.GenreType,
            GenreId = m.GenreId,
            MovieDetails = m.MovieDetails == null ? null: new MovieDetailDto
            {
                Synopsis = m.MovieDetails.Synopsis,
                Language = m.MovieDetails.Language,
                Budget = m.MovieDetails.Budget
            }
        }).AsQueryable().ToPagedResult(paging.CurrentPage, paging.PageSize);
    }
    public async Task<MovieDto?> GetMovieByIdAsync(int id)
    {
        var movie = await _unit.Movies.GetMovieAsync(id);

        if (movie == null)
            return null;

        var movieDto = new MovieDto
        {
            Title = movie.Title,
            Year = movie.Year,
            Duration = movie.Duration,
            Genre = movie.Genre.GenreType
        };
        return movieDto;
    }

    public async Task<AllMovieDetailsDto?> GetMovieDetailsAsync(int id)
    {
        var movie = await _unit.Movies.GetMovieDetailsById(id);

        if (movie == null)
            return null;

        var movieDetailsDto = new AllMovieDetailsDto
        {
            Title = movie.Title,
            Year = movie.Year,
            Duration = movie.Duration,
            Genre = movie.Genre.GenreType,
            Synopsis = movie.MovieDetails.Synopsis,
            Language = movie.MovieDetails.Language,
            Budget = movie.MovieDetails.Budget,
            Reviews = movie.Reviews.Select(r => new ReviewDto { ReviewerName = r.ReviewerName, Comment = r.Comment, Rating = r.Rating }).ToList(),
            Actors = movie.Actors.Select(a => new ActorDto { Name = a.Name, BirthYear = a.BirthYear }).ToList()
        };
   
        return movieDetailsDto;
    }

    public async Task<Movie?> PutMovieAsync(int id, JsonPatchDocument<MovieUpdateDto> patchDoc)
    {
        var movie = await _unit.Movies.GetMovieAsync(id);

        if (movie == null)
            return null;

        var movieAndDetailsDto = new MovieUpdateDto
        {
            Title = movie.Title,
            Year = movie.Year,
            Duration = movie.Duration,
            Genre = movie.Genre.GenreType,
            MovieDetails = new MovieDetailDto
            {
                Synopsis = movie.MovieDetails.Synopsis,
                Language = movie.MovieDetails.Language,
                Budget = movie.MovieDetails.Budget
            }
        };

        patchDoc.ApplyTo(movieAndDetailsDto);

        movie.Title = movieAndDetailsDto.Title;
        movie.Year = movieAndDetailsDto.Year;
        movie.Duration = movieAndDetailsDto.Duration;
        movie.Genre.GenreType = movieAndDetailsDto.Genre;

        movie.MovieDetails.Synopsis = movieAndDetailsDto.MovieDetails.Synopsis;
        movie.MovieDetails.Language = movieAndDetailsDto.MovieDetails.Language;
        movie.MovieDetails.Budget = movieAndDetailsDto.MovieDetails.Budget;

        _unit.Movies.Update(movie);
        await _unit.SaveAsync();

        return movie;

    }

    public async Task<Movie> PostMovieAsync(MovieCreateDto movieCreateDto)
    {
        if (await _unit.Movies.MovieExistsByName(movieCreateDto.Title))
            throw new Exception("Movie with the same title already exists");

        if (movieCreateDto.MovieDetails.Budget < 0)
            throw new Exception("Budget can't be negative");

        if (movieCreateDto.GenreId < 0 || movieCreateDto.GenreId > 8)
            throw new Exception("Genre don't exist!");

        if (movieCreateDto.GenreId == 8) 
        {
            if (movieCreateDto.MovieDetails.Budget < 1_000_000)
                throw new Exception("Budget for Documentary movies must be at least 1 million");
        }


        var movie = new Movie()
        {
            Title = movieCreateDto.Title,
            Year = movieCreateDto.Year,
            Duration = movieCreateDto.Duration,
            GenreId = movieCreateDto.GenreId,
            MovieDetails = new MovieDetails
            {
                Synopsis = movieCreateDto.MovieDetails.Synopsis,
                Language = movieCreateDto.MovieDetails.Language,
                Budget = movieCreateDto.MovieDetails.Budget
            }
        };


        _unit.Movies.Add(movie);
        await _unit.SaveAsync();

        return movie;
    }

    public async Task<Movie?> DeleteMovieAsync(int id)
    {
        var movie = await _unit.Movies.GetMovieAsync(id);

        if (movie == null)
            return null;

        _unit.Movies.Remove(movie);
        await _unit.SaveAsync();

        return movie;

    }
}
