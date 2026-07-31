using Microsoft.EntityFrameworkCore;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Core.Paging;
using MovieApi.Data;
using MovieApi.Services;
using MovieApi.Tests.InterfaceSetups;
using MovieApi.Tests.PagingSetup;

namespace MovieApi.Tests.Services;

public  class ReviewServiceTests
{

    [Fact]
    public async Task GetReviewsSpecifictToMovieId()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new ReviewService(unit);

        var review = new Review
        {
            Id = 1,
            ReviewerName = "Alice",
            MovieId = 1
        };

        var genre = new Genre
        {
            Id = 1,
            GenreType = "Sci-Fi"
        };

        var movieReviews = new Movie
        {
            Id = 1,
            Title = "Inception",
            Year = new DateTime(2010, 7, 16),
            Duration = 148,
            GenreId = 1
        };
        context.Genres.Add(genre);
        context.Reviews.Add(review);
        context.Movies.Add(movieReviews);
        await context.SaveChangesAsync();

        var paging = new PagingParameters
        {
            CurrentPage = 1,
            PageSize = 10
        };

        // Act
        var result = await service.GetReviewsForSpecificMovieAsync(1, paging);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Alice", result.Data.Single().ReviewerName);

    }

    [Fact]
    public async Task GetAllReviews()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new ReviewService(unit);

        context.Reviews.AddRange(
            new Review
            {
                Id = 1,
                ReviewerName = "Alice",
                Comment = "A mind-bending masterpiece!",
                Rating = 5
            },
            new Review
            {
                Id = 2,
                ReviewerName = "Charlie",
                Comment = "An iconic crime drama.",
                Rating = 5
            });

        await context.SaveChangesAsync();

        var paging = new PagingParameters
        {
            CurrentPage = 1,
            PageSize = 10
        };

        // Act
        var result = await service.GetReviewsAsync(paging);

        // Assert
        Assert.NotNull(result);

        Assert.Equal(2, result.Data.Count());

        var checkReview = result.Data.First();

        Assert.NotNull(checkReview);
        Assert.Equal("Alice", checkReview.ReviewerName);
        Assert.Equal("A mind-bending masterpiece!", checkReview.Comment);
        Assert.Equal(5, checkReview.Rating);
    }

    [Fact]
    public async Task CreateReview()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new ReviewService(unit);

        context.Movies.Add(new Movie
        {
            Id = 1,
            Title = "Inception",
            Year = new DateTime(2010, 7, 16),
            Duration = 148,
            GenreId = 1 
        });
        var genre = new Genre
        {
            Id = 1,
            GenreType = "Sci-Fi"
        };
        context.Genres.Add(genre);

        await context.SaveChangesAsync();

        var reviewDto = new ReviewDto
        {
            ReviewerName = "Alice",
            Comment = "Really cool movie! Especially that action trick!",
            Rating = 5
        };

        // Act
        var result = await service.PostReviewAsync(1, reviewDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Alice", result.ReviewerName);
        Assert.Equal("Really cool movie! Especially that action trick!", result.Comment);
        Assert.Equal(5, result.Rating);

    }

    [Fact]
    public async Task DeleteReview()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MovieApiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new MovieApiContext(options);

        var unit = new TestSetupUnitOfWork(context);
        var service = new ReviewService(unit);

        context.Reviews.Add(new Review
        {
            Id = 1,
            ReviewerName = "Alice",
            Comment = "Really cool movie! Especially that action trick!",
            Rating = 5
        });

        // Act
        var result = await service.DeleteReviewAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);

        var checkIfReviewExist = await context.Reviews.FindAsync(1);
        Assert.Null(checkIfReviewExist);

        Assert.Empty(context.Reviews);
    }
}
