using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data;

public class MovieApiContext(DbContextOptions<MovieApiContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<MovieDetails> MovieDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}

