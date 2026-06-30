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

public class ActorsControllerTests
{
    [Fact]
    public async Task GetAllActorsThatExistInAList()
    {
        // Arrange
        var actorDtoList = new List<ActorDto> { new ActorDto
        {
            Name = "Leonardo DiCaprio",
            BirthYear = new DateTime(1974, 11, 11)
        },
        new ActorDto
        {
            Name = "Keanu Reeves",
            BirthYear = new DateTime(1964, 09, 02)
        },
        new ActorDto
        {
            Name = "Marlon Brando",
            BirthYear = new DateTime(1924, 4, 3)
        },
        new ActorDto
        {
            Name = "Christian Bale",
            BirthYear = new DateTime(1974, 1, 30)
        }};
        var mockService = new Mock<IActorService>();
        mockService.Setup(s => s.GetActorsAsync())
            .ReturnsAsync(actorDtoList);

        var controller = new ActorsController(mockService.Object);

        // Act
        var result = await controller.GetActors();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actor = Assert.IsType<List<ActorDto>>(okResult.Value);

        // Assert
        Assert.Equal(4, actor.Count);
    }

    [Fact]
    public async Task GetActorById()
    {
        // Arrange

        var actorDto = new ActorDto
        {
            Name = "Leonardo DiCaprio",
            BirthYear = new DateTime(1974, 11, 11)
        };
        var mockService = new Mock<IActorService>();
        mockService.Setup(s => s.GetActorByIdAsync(1))
        .ReturnsAsync(actorDto);
        var controller = new ActorsController(mockService.Object);

        // Act
        var result = await controller.GetActorById(1);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actor = Assert.IsType<ActorDto>(okResult.Value);

        // Assert
        Assert.Equal("Leonardo DiCaprio", actor.Name);
    }

    [Fact]
    public async Task UpdateActor()
    {
        // Arrange
        var actorDto = new ActorDto
        {
            Name = "Leonardo DiCaprio",
            BirthYear = new DateTime(1974, 11, 11)
        };

        var actor = new Actor
        {
            Id = 1,
            Name = "Leonardo DiCaprio",
            BirthYear = new DateTime(1974, 11, 11)
        };

        var mockService = new Mock<IActorService>();
        mockService.Setup(s => s.PutActorAsync(1, actorDto))
            .ReturnsAsync(actor);
        var controller = new ActorsController(mockService.Object);

        // Act
        var result = await controller.PutActor(1, actorDto);
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var actorResult = Assert.IsType<Actor>(okResult.Value);

        // Assert
        Assert.Equal("Leonardo DiCaprio", actorResult.Name);
    }

    [Fact]
    public async Task CreateActor()
    {
        // Arrange
        var actorDto = new ActorDto
        {
            Name = "Leonardo DiCaprio",
            BirthYear = new DateTime(1974, 11, 11)
        };

        var actor = new Actor
        {
            Id = 1,
            Name = "Leonardo DiCaprio",
            BirthYear = new DateTime(1974, 11, 11)
        };

        var mockService = new Mock<IActorService>();
        mockService.Setup(s => s.PostActorAsync(actorDto))
            .ReturnsAsync(actor);
        var controller = new ActorsController(mockService.Object);

        // Act
        var result = await controller.PostActor(actorDto);
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var checkActor = Assert.IsType<Actor>(createdResult.Value);

        // Assert
        Assert.Equal(1, checkActor.Id);
        Assert.Equal("Leonardo DiCaprio", checkActor.Name);
    }

    [Fact]
    public async Task AddActorToMovie()
    {
        // Arrange
        var actor = new Actor
        {
            Id = 1,
            Name = "Leonardo DiCaprio",
            BirthYear = new DateTime(1974, 11, 11)
        };

        var mockService = new Mock<IActorService>();
        mockService.Setup(s => s.AddActorToMovieAsync(1, 2))
            .ReturnsAsync(actor);
        var controller = new ActorsController(mockService.Object);

        // Act
        var result = await controller.AddActorToMovie(1, 2);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task DeleteActor()
    {
        // Arrange
        var actor = new Actor
        {
            Id = 1,
            Name = "Leonardo DiCaprio",
            BirthYear = new DateTime(1974, 11, 11)
        };
        var mockService = new Mock<IActorService>();
        mockService.Setup(s => s.DeleteActorAsync(1))
            .ReturnsAsync(actor);
        var controller = new ActorsController(mockService.Object);

        // Act
        var result = await controller.DeleteActor(1);
        
        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }
}
