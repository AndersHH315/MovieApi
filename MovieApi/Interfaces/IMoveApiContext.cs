using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Interfaces
{
    public interface IMovieApiContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Actor> Actors { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<MovieDetails> MovieDetails { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}