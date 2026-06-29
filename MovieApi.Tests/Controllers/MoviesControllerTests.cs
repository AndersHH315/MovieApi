using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Controllers;
using MovieApi.DTOs;
using MovieApi.Interfaces;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Tests.Controllers
{
    public class MoviesControllerTests
    {
        [Fact]
        public async Task Get_Id_ReturnsOkWithMovie()
        {
            // Arrange
            var movieDto = new MovieDto
            {
                Title = "Inception",
                Year = new DateTime(2010, 07, 16),
                Duration = 148
            };
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMovieByIdAsync(1))
            .ReturnsAsync(movieDto);
            var controller = new MoviesController(mockService.Object);

            // Act
            var result = await controller.GetMovieById(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movie = Assert.IsType<MovieDto>(okResult.Value);

            // Assert
            Assert.Equal("Inception", movie.Title);
        }

        [Fact]
        public async Task GetAllMoviesAsync()
        {
            // Arrange
            var movieDtoList = new List<MovieDto> { new MovieDto
            {
                Title = "Inception",
                Year = new DateTime(2010, 07, 16),
                Duration = 148
            },
            new MovieDto
            {
                Title = "The Dark Knight",
                Year = new DateTime(2008, 07, 18),
                Duration = 152
            },
            new MovieDto
            {
                Title = "The Matrix",
                Year = new DateTime(1999, 03, 31),
                Duration = 136
            },
            new MovieDto
            {
                Title = "The Godfather",
                Year = new DateTime(1972, 03, 24),
                Duration = 175
            }};

            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetAllMoviesAsync())
                .ReturnsAsync(movieDtoList);
            var controller = new MoviesController(mockService.Object);

            // Act
            var result = await controller.GetMovies();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movie = Assert.IsType<List<MovieDto>>(okResult.Value);

            // Assert
            Assert.Equal(4, movie.Count);
        }

        [Fact]
        public async Task GetMovieDetails()
        {
            // Arrange
            var movieDetailDto = new MovieDetailDto
                {
                    Title = "Inception",
                    Year = new DateTime(2010, 07, 16),
                    Duration = 148,
                    Genre = "Sci-Fi",
                    Synopsis = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.",
                    Language = "English",
                    Budget = 16000000,
                    Reviews = new List<ReviewDto>() { new ReviewDto { ReviewerName = "Alice", Rating = 5, Comment = "A mind-bending masterpiece!" } },
                    Actors = new List<ActorDto>() { new ActorDto { Name = "Leonardo DiCaprio", BirthYear = new DateTime(1974, 11, 11) } }
                };

            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMovieDetailsAsync(1))
                .ReturnsAsync(movieDetailDto);
            var controller = new MoviesController(mockService.Object);

            // Act
            var result = await controller.GetMovieDetails(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movie = Assert.IsType<MovieDetailDto>(okResult.Value);

            // Assert
            Assert.Equal("Inception", movie.Title);
        }

        [Fact]
        public async Task PutMovieAsync()
        {
            // Arrange
            var movieUpdateDto = new MovieUpdateDto
            {
                Title = "Inception",
                Year = new DateTime(2010, 07, 16),
                Duration = 148
            };

            var movie = new Movie
            {
                Id = 1,
                Title = "Inception",
                Year = new DateTime(2010, 07, 16),
                Duration = 148
            };

            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.PutMovieAsync(1, movieUpdateDto))
                .ReturnsAsync(movie);
            var controller = new MoviesController(mockService.Object);

            // Act
            var result = await controller.PutMovie(1, movieUpdateDto);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movieResult = Assert.IsType<Movie>(okResult.Value);

            // Assert
            Assert.Equal(148, movieResult.Duration);
        }

        [Fact]
        public async Task PostMovieAsync()
        {
            // Arrange
            var movieDto = new MovieDto
            {
                Title = "Inception",
                Year = new DateTime(2010, 07, 16),
                Duration = 148
            };

            var movie = new Movie
            {
                Id = 1,
                Title = "Inception",
                Year = new DateTime(2010, 07, 16),
                Duration = 148
            };

            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.PostMovieAsync(movieDto))
            .ReturnsAsync(movie);
            var controller = new MoviesController(mockService.Object);

            // Act
            var result = await controller.PostMovie(movieDto);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var checkMovie = Assert.IsType<Movie>(createdResult.Value);

            // Assert
            Assert.Equal(1, checkMovie.Id);
            Assert.Equal("Inception", checkMovie.Title);
        }

        [Fact]
        public async Task DeleteMovie()
        {
            // Arrange
            var movie = new Movie
            {
                Id = 1,
                Title = "Inception",
                Year = new DateTime(2010, 07, 16),
                Duration = 148

            };
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.DeleteMovieAsync(1))
            .ReturnsAsync(movie);
            var controller = new MoviesController(mockService.Object);

            // Act
            var result = await controller.DeleteMovie(1);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }
    }
}

