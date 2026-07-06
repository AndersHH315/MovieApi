using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Core.DomainContracts;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Services.Contracts;

namespace MovieApi.Services;

public class MovieService(IUnitOfWork unit) : IMovieService
{
    private readonly IUnitOfWork _unit = unit;

    public async Task<IEnumerable<MovieDto?>?> GetAllMoviesAsync()
    {
        var movies = await _unit.Movies.GetAllAsync();
        return movies.Select(m => new MovieDto
        {
            Title = m.Title,
            Year = m.Year,
            Duration = m.Duration,
            Genre = m.Genre.GenreType
        });
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

    public async Task<MovieDetailDto?> GetMovieDetailsAsync(int id)
    {
        var movie = await _unit.Movies.GetMovieDetailsById(id);

        if (movie == null)
            return null;

        var movieDetailsDto = new MovieDetailDto
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

    public async Task<Movie?> PutMovieAsync(int id, MovieUpdateDto movieDto)
    {
        var movie = await _unit.Movies.GetMovieAsync(id);

        if (movie == null)
            return null;

        movie.Title = movieDto.Title;
        movie.Year = movieDto.Year;
        movie.Duration = movieDto.Duration;
        movie.Genre.GenreType = movieDto.Genre;

        _unit.Movies.Update(movie);
        await _unit.SaveAsync();

        return movie;

    }

    public async Task<Movie> PostMovieAsync(MovieCreateDto movieCreateDto)
    {
        var movie = new Movie()
        {
            Title = movieCreateDto.Title,
            Year = movieCreateDto.Year,
            Duration = movieCreateDto.Duration,
            GenreId = movieCreateDto.GenreId
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
