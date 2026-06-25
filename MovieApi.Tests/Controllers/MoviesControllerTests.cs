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
            var mockService = new Mock<IMovieService>();
            mockService.Setup(s => s.GetMovieByIdAsync(1))
            .ReturnsAsync(new MovieDto { Title = "Inception"});
            var controller = new MoviesController(mockService.Object);
            var result = await controller.GetMovieById(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var movie = Assert.IsType<MovieDto>(okResult.Value);
            Assert.Equal("Inception", movie.Title);
        }
    }
}
