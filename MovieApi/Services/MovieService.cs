using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.DTOs;
using MovieApi.Interfaces;
using MovieApi.Models;

namespace MovieApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieApiContext _db;
        public MovieService(IMovieApiContext db)
        {
            _db = db;
        }
        public async Task<MovieDto?> GetMovieByIdAsync(int id)
        {
            var movie = await _db.Movies
                .Include(g => g.Genres)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return null;

            var movieDto = new MovieDto
            {
                Title = movie.Title,
                Year = movie.Year,
                Duration = movie.Duration,
                Genre = movie.Genres?.GenreType ?? "No Genre"
            };
            return movieDto;
        }
        public async Task<IEnumerable<MovieDto?>?> GetAllMoviesAsync()
        {
            var movies = await _db.Movies.Select(m => new MovieDto
                {
                    Title = m.Title,
                    Year = m.Year,
                    Duration = m.Duration,
                    Genre = m.Genres != null ? m.Genres.GenreType : null
                }).ToListAsync();
            if (movies.Count == 0)
                return null;

            return movies;
        }

        public async Task<MovieDetailDto?> GetMovieDetailsAsync(int id)
        {
            var movie = await _db.Movies
                .Include(r => r.Reviews)
                .Include(a => a.Actors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return null;

            var movieGenre = await _db.Genres
                .Where(g => g.Id == movie.GenreId).Select(g => g.GenreType)
                .FirstOrDefaultAsync();

            if (movieGenre == null)
                return null;

            var movieDetails = await _db.MovieDetails
                .Where(m => m.MovieId == movie.Id)
                .FirstOrDefaultAsync();

            if (movieDetails == null)
                return null;

            var movieDetailsDto = new MovieDetailDto
            {
                Title = movie.Title,
                Year = movie.Year,
                Duration = movie.Duration,
                Genre = movieGenre,
                Synopsis = movieDetails.Synopsis,
                Language = movieDetails.Language,
                Budget = movieDetails.Budget,
                Reviews = movie.Reviews.Select(r => new ReviewDto { ReviewerName = r.ReviewerName, Comment = r.Comment, Rating = r.Rating }).ToList(),
                Actors = movie.Actors.Select(a => new ActorDto { Name = a.Name, BirthYear = a.BirthYear }).ToList()
            };
       
            return movieDetailsDto;
        }

        public async Task<Movie?> PutMovieAsync(int id, MovieUpdateDto movieDto)
        {
            var movie = await _db.Movies.FindAsync(id);

            if (movie == null)
                return null;

            movie.Title = movieDto.Title;
            movie.Year = movieDto.Year;
            movie.Duration = movieDto.Duration;

            var genre = await _db.Genres
                .Where(g => g.GenreType == movieDto.Genre)
                .FirstOrDefaultAsync();

            if (genre != null)
                movie.GenreId = genre.Id;

            _db.Movies.Entry(movie).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return movie;

        }

        public async Task<Movie> PostMovieAsync(MovieDto movieDto)
        {
            var movie = new Movie()
            {
                Title = movieDto.Title,
                Year = movieDto.Year,
                Duration = movieDto.Duration,
                GenreId = _db.Genres.Where(g => g.GenreType == movieDto.Genre).Select(g => g.Id).FirstOrDefault()
            };

            _db.Movies.Add(movie);
            await _db.SaveChangesAsync();

            return movie;
        }

        public async Task<Movie?> DeleteMovieAsync(int id)
        {
            var movie = await _db.Movies.FindAsync(id);

            if (movie == null)
                return null;

            _db.Movies.Remove(movie);
            await _db.SaveChangesAsync();

            return movie;

        }
    }
}
