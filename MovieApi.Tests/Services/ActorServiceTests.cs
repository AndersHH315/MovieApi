using Microsoft.EntityFrameworkCore;
using MovieApi.Core.Models;
using MovieApi.Data;
using MovieApi.Core.DTOs;
using MovieApi.Services;
using MovieApi.Tests.InterfaceSetups;
using MovieApi.Core.Paging;

namespace MovieApi.Tests.Services
{
    public class ActorServiceTests
    {
        [Fact]
        public async Task GetActorById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MovieApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new MovieApiContext(options);

            var unit = new TestSetupUnitOfWork(context);
            var service = new ActorService(unit);


            context.Actors.Add(new Actor
            {
                Id = 1,
                Name = "Leonardo DiCaprio",
                BirthYear = new DateTime(1974, 11, 11)
            });
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetActorByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Leonardo DiCaprio", result.Name);
            Assert.Equal(new DateTime(1974, 11, 11), result.BirthYear);

        }

        [Fact]
        public async Task GetAllActor()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MovieApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new MovieApiContext(options);

            var unit = new TestSetupUnitOfWork(context);
            var service = new ActorService(unit);

            context.Actors.AddRange(
                new Actor
                {
                    Id = 1,
                    Name = "Leonardo DiCaprio",
                    BirthYear = new DateTime(1974, 11, 11)
                },
                new Actor
                {
                    Id = 2,
                    Name = "Keanu Reeves",
                    BirthYear = new DateTime(1964, 09, 02)
                });

            await context.SaveChangesAsync();

            var paging = new PagingParameters
            {
                CurrentPage = 1,
                PageSize = 10
            };

            // Act
            var result = await service.GetActorsAsync(paging);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(2, result.Data.Count());

            var checkActor = result.Data.First();

            Assert.NotNull(checkActor);
            Assert.Equal("Leonardo DiCaprio", checkActor.Name);
            Assert.Equal(new DateTime(1974, 11, 11), checkActor.BirthYear);
        }

        [Fact]
        public async Task CreateActor()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MovieApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new MovieApiContext(options);

            var unit = new TestSetupUnitOfWork(context);
            var service = new ActorService(unit);

            var actor = new ActorDto
            {
                Name = "Leonardo DiCaprio",
                BirthYear = new DateTime(1974, 11, 11)
            };

            // Act
            var result = await service.PostActorAsync(actor);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Leonardo DiCaprio", result.Name);
            Assert.Equal(new DateTime(1974, 11, 11), result.BirthYear);

        }

        [Fact]
        public async Task UpdateActor()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MovieApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new MovieApiContext(options);

            var unit = new TestSetupUnitOfWork(context);
            var service = new ActorService(unit);

            context.Actors.Add(
            new Actor
            {
                Id = 1,
                Name = "Leonardo DiCaprio",
                BirthYear = new DateTime(1974, 11, 11)
            });

            await context.SaveChangesAsync();

            var actorUpdate = new ActorDto
            {
                Name = "Keanu Reeves",
                BirthYear = new DateTime(1964, 09, 02)
            };

            // Act
            var result = await service.PutActorAsync(1, actorUpdate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Keanu Reeves", result.Name);
            Assert.Equal(new DateTime(1964, 09, 02), result.BirthYear);
        }


        [Fact]
        public async Task AddActorToMovie()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MovieApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new MovieApiContext(options);

            var unit = new TestSetupUnitOfWork(context);
            var service = new ActorService(unit);

            context.Genres.Add(new Genre
            {
                Id = 1,
                GenreType = "Sci-Fi"
            });

            var actor = new Actor
            {
                Id = 1,
                Name = "Leonardo DiCaprio",
                BirthYear = new DateTime(1974, 11, 11)
            };

            var movie = new Movie
            {
                Id = 1,
                Title = "Inception",
                Year = new DateTime(2010, 7, 16),
                Duration = 148,
                GenreId = 1,
                Actors = new List<Actor>()
            };

            context.Actors.Add(actor);
            context.Movies.Add(movie);
            await context.SaveChangesAsync();

            // Act
            var result = service.AddActorToMovieAsync(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(actor.Id, result.Id);

            var checkIfActorInMovie = await context.Movies
                .Include(m => m.Actors)
                .FirstAsync(m => m.Id == 1);

            Assert.Single(checkIfActorInMovie.Actors);
            Assert.Contains(checkIfActorInMovie.Actors, a => a.Id == actor.Id);

        }

        [Fact]
        public async Task DeleteActor()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MovieApiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new MovieApiContext(options);

            var unit = new TestSetupUnitOfWork(context);
            var service = new ActorService(unit);

            context.Actors.Add(new Actor
            {
                Id = 1,
                Name = "Leonardo DiCaprio",
                BirthYear = new DateTime(1974, 11, 11)
            });

            // Act
            var result = await service.DeleteActorAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);

            var checkIfActorExist = await context.Actors.FindAsync(1);
            Assert.Null(checkIfActorExist);

            Assert.Empty(context.Actors);
        }
    }
}
