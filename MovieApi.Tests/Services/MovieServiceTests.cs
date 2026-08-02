using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Core.Paging;
using MovieApi.Data;
using MovieApi.Services;
using MovieApi.Tests.InterfaceSetups;

namespace MovieApi.Tests.Services;

public class MovieServiceTests
{
    [Fact]
    public async Task GetMovieById()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new MovieService(unit);

        context.Genres.Add(new Genre
        {
            Id = 1,
            GenreType = "Sci-Fi"
        });

        context.Movies.Add(new Movie
        {
            Id = 1,
            Title = "Inception",
            Year = new DateTime(2010, 7, 16),
            Duration = 148,
            GenreId = 1
        });

        await context.SaveChangesAsync();

        // Act
        var result = await service.GetMovieByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Inception", result.Title);
        Assert.Equal(new DateTime(2010, 7, 16), result.Year);
        Assert.Equal(148, result.Duration);


    }

    [Fact]
    public async Task GetAllMovies()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new MovieService(unit);

        context.Genres.Add( new Genre
        {
            Id = 1,
            GenreType = "Sci-Fi"
        });

        context.Movies.AddRange(
         new Movie
        {
             Id = 1,
             Title = "Inception",
             Year = new DateTime(2010, 7, 16),
             Duration = 148,
             GenreId = 1
        },
        new Movie
        {
            Id = 2,
            Title = "The Dark Knight",
            Year = new DateTime(2008, 07, 18),
            Duration = 152,
            GenreId = 1
        });

        await context.SaveChangesAsync();

        var paging = new PagingParameters
        {
            CurrentPage = 1,
            PageSize = 10
        };

        // Act
        var result = await service.GetAllMoviesAsync(paging);

        // Assert
        Assert.NotNull(result);

        Assert.Equal(2, result.Data.Count());

        var checkMovie = result.Data.First();

        Assert.NotNull(checkMovie);
        Assert.Equal("Inception", checkMovie.Title);
        Assert.Equal(new DateTime(2010, 7, 16), checkMovie.Year);
        Assert.Equal(148, checkMovie.Duration);
    }

    [Fact]
    public async Task GetMovieDetails()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new MovieService(unit);

        context.Genres.Add(new Genre
        {
            Id = 1,
            GenreType = "Sci-Fi"
        });

        context.Movies.Add(new Movie
        {
            Id = 1,
            Title = "Inception",
            Year = new DateTime(2010, 7, 16),
            Duration = 148,
            GenreId = 1
        });

        context.MovieDetails.Add(new MovieDetails
        {
            Synopsis = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
            Language = "English",
            Budget = 16000000,
            MovieId = 1
        });

        context.Reviews.Add(new Review
        {
            Id = 1,
            ReviewerName = "Alice",
            Comment = "Really cool movie! Especially that action trick!",
            Rating = 5,
            MovieId = 1
        });

        await context.SaveChangesAsync();

        // Act
        var result = await service.GetMovieDetailsAsync(1);

        // Assert
        Assert.NotNull(result);

        Assert.Equal("Inception", result.Title);
        Assert.Equal("English", result.Language);
        Assert.Equal(16000000, result.Budget);
        Assert.Single(result.Reviews);
    }

    [Fact]
    public async Task CreateMovie()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new MovieService(unit);

        var movie = new MovieCreateDto
        {
            Title = "Inception",
            Year = new DateTime(2010, 7, 16),
            Duration = 148
        };

        // Act
        var result = await service.PostMovieAsync(movie);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Inception", result.Title);
        Assert.Equal(new DateTime(2010, 7, 16), result.Year);
        Assert.Equal(148, result.Duration);

    }

    [Fact]
    public async Task UpdateMovie()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new MovieService(unit);

        context.Genres.Add(new Genre
        {
            Id = 1,
            GenreType = "Sci-Fi"
        });

        context.Movies.Add(
         new Movie
        {
            Id = 1,
            Title = "Inception",
            Year = new DateTime(2010, 7, 16),
            Duration = 148,
            GenreId = 1,
            MovieDetails = new MovieDetails {
                Synopsis = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                Language = "English",
                Budget = 16000000
            }
        });

        await context.SaveChangesAsync();

        var patchDoc = new JsonPatchDocument<MovieUpdateDto>();

        patchDoc.Replace(m => m.Title, "Interstellar");
        patchDoc.Replace(m => m.Year, new DateTime(2014, 11, 7));
        patchDoc.Replace(m => m.Duration, 169);

        // Act
        var result = await service.PutMovieAsync(1, patchDoc);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Interstellar", result.Title);
        Assert.Equal(new DateTime(2014, 11, 7), result.Year);
        Assert.Equal(169, result.Duration);

    }

    [Fact]
    public async Task DeleteMovie()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new MovieService(unit);

        context.Genres.Add(new Genre
        {
            Id = 1,
            GenreType = "Sci-Fi"
        });

        context.Movies.Add(new Movie
        {
            Id = 1,
            Title = "Inception",
            Year = new DateTime(2010, 7, 16),
            Duration = 148,
            GenreId = 1

        });

        await unit.SaveAsync();
        // Act
        var result = await service.DeleteMovieAsync(1);


        // Assert
        Assert.NotNull(result);

        var checkIfMovieExist = await context.Movies.FindAsync(1);
        Assert.Null(checkIfMovieExist);

        Assert.Empty(context.Movies);
    }
}
