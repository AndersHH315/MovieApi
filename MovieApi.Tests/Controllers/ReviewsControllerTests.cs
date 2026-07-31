using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Controllers;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Core.Paging;
using MovieApi.Services.Contracts;
using MovieApi.Tests.PagingSetup;
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
        var pagedReviews = TestPagedResult.Create(reviewDtoList);
        var paging = new PagingParameters
        {
            CurrentPage = 1,
            PageSize = 10
        };
        var mockService = new Mock<IReviewService>();
        mockService.Setup(s => s.GetReviewsAsync(It.IsAny<PagingParameters>()))
            .ReturnsAsync(pagedReviews);
        var controller = new ReviewsController(mockService.Object);

        // Act
        var result = await controller.GetReviews(paging);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var review = Assert.IsType<PagedResult<ReviewDto>>(okResult.Value);

        // Assert
        Assert.Equal(4, review.Data.Count());
    }

    [Fact]
    public async Task GetReviewsSpecifictToMovieId()
    {
        // Arrange
        var movieReviews = new List<ReviewDto>
        {
            new ReviewDto
            {
                ReviewerName = "Alice",
            },
            new ReviewDto
            {
                ReviewerName = "Bob",
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

        var review = new ReviewDto
        {
            ReviewerName = "Alice",
            Comment = "Really cool movie! Especially that action trick!",
            Rating = 5,
        };

        var mockService = new Mock<IReviewService>();
        mockService.Setup(s => s.PostReviewAsync(1, reviewDto))
            .ReturnsAsync(review);
        var controller = new ReviewsController(mockService.Object);

        // Act
        var result = await controller.PostReview(1, reviewDto);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var checkReview = Assert.IsType<ReviewDto>(createdResult.Value);

        // Assert
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
        Assert.IsType<NoContentResult>(result);

    }

}
