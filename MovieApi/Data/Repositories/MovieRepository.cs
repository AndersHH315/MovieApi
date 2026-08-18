using Microsoft.EntityFrameworkCore;
using MovieApi.Core.DomainContracts;
using MovieApi.Core.Models;
using MovieApi.Services.Contracts;

namespace MovieApi.Data.Repositories;

public class MovieRepository(MovieApiContext db) : IMovieRepository
{
    private readonly MovieApiContext _db = db;
    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        var movies = await _db.Movies
            .Include(m => m.Genre)
            .Include(m => m.MovieDetails)
            .ToListAsync();
        return movies;
    }
    public async Task<Movie?> GetMovieAsync(int id)
    {
        var movie = await _db.Movies
            .Include(m => m.Genre)
            .Include(m => m.MovieDetails)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return null;
        return movie;
    }

    public async Task<Movie?> GetMovieDetailsById(int id)
    {
        return await _db.Movies
           .Include(m => m.Genre)
           .Include(m => m.MovieDetails)
           .Include(m => m.Reviews)
           .Include(m => m.Actors)
           .FirstOrDefaultAsync(m => m.Id == id);
    }
    public void Add(Movie movie)
    {
        _db.Movies.Add(movie);
    }
    public void Update(Movie movie)
    {
        _db.Movies.Update(movie);
    }
    public void Remove(Movie movie)
    {
        _db.Movies.Remove(movie);
    }
    public async Task<bool>MovieExistsByName(string title)
    {
        return await _db.Movies.AnyAsync(x => x.Title == title);
    }

}
