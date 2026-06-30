using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Controllers;
using MovieApi.DTOs;
using MovieApi.Interfaces;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Tests.Controllers;

public class ReviewsControllerTests
{
    [Fact]
    public async Task GetReviewsToList()
    {
        // Arrange
        var reviewDtoList = new List<ReviewDto> { new ReviewDto
        {
            ReviewerName = "Alice",
            Comment = "A mind-bending masterpiece!",
            Rating = 5
        },
        new ReviewDto
        {
            ReviewerName = "Bob",
            Comment = "A groundbreaking sci-fi classic.",
            Rating = 4
        },
        new ReviewDto
        {
            ReviewerName = "Charlie",
            Comment = "An iconic crime drama.",
            Rating = 5
        },
        new ReviewDto
        {
            ReviewerName = "Dave",
            Comment = "A thrilling superhero film.",
            Rating = 5
        }};
        var mockService = new Mock<IReviewService>();
        mockService.Setup(s => s.GetReviewsAsync())
            .ReturnsAsync(reviewDtoList);
        var controller = new ReviewsController(mockService.Object);

        // Act
        var result = await controller.GetReviews();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var review = Assert.IsType<List<ReviewDto>>(okResult.Value);

        // Assert
        Assert.Equal(4, review.Count);
    }

    [Fact]
    public async Task GetReviewsSpecifictToMovieId()
    {
        // Arrange
        var movieReviews = new List<Review>
        {
            new Review
            {
                Id = 1,
                ReviewerName = "Alice",
                MovieId = 1
            },
            new Review
            {
                Id = 2,
                ReviewerName = "Bob",
                MovieId = 1
            }
        };
       
        var mockService = new Mock<IReviewService>();
        mockService.Setup(s => s.GetReviewsForSpecificMovieAsync(1))
            .ReturnsAsync(movieReviews);
        var controller = new ReviewsController(mockService.Object);

        // Act
        var result = await controller.GetReviewsForSpecificMovie(1);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var review = Assert.IsAssignableFrom<IEnumerable<ReviewDto>>(okResult.Value);

        // Assert
        Assert.Equal(2, review.Count());
        Assert.Contains(review, r => r.ReviewerName == "Alice");

    }

    [Fact]
    public async Task CreateReview()
    {
        // Arrange
        var reviewDto = new ReviewDto
        {
            ReviewerName = "Alice",
            Comment = "Really cool movie! Especially that action trick!",
            Rating = 5
        };

        var review = new Review
        {
            Id = 1,
            ReviewerName = "Alice",
            Comment = "Really cool movie! Especially that action trick!",
            Rating = 5,
            MovieId = 1
        };

        var mockService = new Mock<IReviewService>();
        mockService.Setup(s => s.PostReviewAsync(1, reviewDto))
            .ReturnsAsync(review);
        var controller = new ReviewsController(mockService.Object);

        // Act
        var result = await controller.PostReview(1, reviewDto);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var checkReview = Assert.IsType<Review>(createdResult.Value);

        // Assert
        Assert.Equal(1, checkReview.MovieId);
        Assert.Equal("Alice", checkReview.ReviewerName);
    }

    [Fact]
    public async Task DeleteReview()
    {
        // Arrange
        var review = new Review
        {
            Id = 1,
            ReviewerName = "Alice",
            Comment = "Really cool movie! Especially that action trick!",
            Rating = 5,
            MovieId = 1
        };
        var mockService = new Mock<IReviewService>();
        mockService.Setup(s => s.DeleteReviewAsync(1))
            .ReturnsAsync(review);
        var controller = new ReviewsController(mockService.Object);

        // Act
        var result = await controller.DeleteReview(1);

        // Assert
        Assert.IsType<NoContentResult>(result.Result);

    }

}
