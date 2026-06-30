using Microsoft.EntityFrameworkCore;
using MovieApi.Interfaces;
using MovieApi.Models;

namespace MovieApi.Data;

public class MovieApiContext(DbContextOptions<MovieApiContext> options) : DbContext(options), IMovieApiContext
{

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<MovieDetails> MovieDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, GenreType = "Action" },
            new Genre { Id = 2, GenreType = "Sci-Fi" },
            new Genre { Id = 3, GenreType = "Drama" },
            new Genre { Id = 4, GenreType = "Comedy" },
            new Genre { Id = 5, GenreType = "Horror" },
            new Genre { Id = 6, GenreType = "Romance" },
            new Genre { Id = 7, GenreType = "Thriller" }
           );

        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Inception", Year = new DateTime(2010, 7, 16), GenreId = 2, Duration = 148 },
            new Movie { Id = 2, Title = "The Matrix", Year = new DateTime(1999, 3, 31), GenreId = 2, Duration = 136 },
            new Movie { Id = 3, Title = "The Godfather", Year = new DateTime(1972, 3, 24), GenreId = 3, Duration = 175 },
            new Movie { Id = 4, Title = "The Dark Knight", Year = new DateTime(2008, 7, 18), GenreId = 1, Duration = 152 },
            new Movie { Id = 5, Title = "Pulp Fiction", Year = new DateTime(1994, 10, 14), GenreId = 3, Duration = 154 }
            );

        modelBuilder.Entity<Actor>().HasData(
            new Actor { Id = 1, Name = "Leonardo DiCaprio", BirthYear = new DateTime(1974, 11, 11) },
            new Actor { Id = 2, Name = "Keanu Reeves", BirthYear = new DateTime(1964, 9, 2) },
            new Actor { Id = 3, Name = "Marlon Brando", BirthYear = new DateTime(1924, 4, 3) },
            new Actor { Id = 4, Name = "Christian Bale", BirthYear = new DateTime(1974, 1, 30) },
            new Actor { Id = 5, Name = "John Travolta", BirthYear = new DateTime(1954, 2, 18) }
            );

        modelBuilder.Entity<MovieDetails>().HasData(
            new MovieDetails { Id = 1, MovieId = 1, Synopsis = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.", Language = "English", Budget = 16000000 },
            new MovieDetails { Id = 2, MovieId = 2, Synopsis = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", Language = "English", Budget = 63000000 },
            new MovieDetails { Id = 3, MovieId = 3, Synopsis = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", Language = "English", Budget = 6000000 },
            new MovieDetails { Id = 4, MovieId = 4, Synopsis = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.", Language = "English", Budget = 185000000 },
            new MovieDetails { Id = 5, MovieId = 5, Synopsis = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", Language = "English", Budget = 8000000 }

            );
        
        modelBuilder.Entity<Review>().HasData(
            new Review { Id = 1, MovieId = 1, ReviewerName = "Alice", Rating = 5, Comment = "A mind-bending masterpiece!" },
            new Review { Id = 2, MovieId = 2, ReviewerName = "Bob", Rating = 4, Comment = "A groundbreaking sci-fi classic." },
            new Review { Id = 3, MovieId = 3, ReviewerName = "Charlie", Rating = 5, Comment = "An iconic crime drama." },
            new Review { Id = 4, MovieId = 4, ReviewerName = "Dave", Rating = 5, Comment = "A thrilling superhero film." },
            new Review { Id = 5, MovieId = 5, ReviewerName = "Eve", Rating = 4, Comment = "A stylish and influential film." }
            );
    }
}

