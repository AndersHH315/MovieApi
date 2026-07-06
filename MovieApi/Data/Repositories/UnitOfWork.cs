using Microsoft.AspNetCore.Mvc;
using MovieApi.Core.DomainContracts;
using MovieApi.Core.DTOs;
using MovieApi.Services.Contracts;

namespace MovieApi.Data.Repositories;
// Add Genre and movieDetails
public class UnitOfWork : IUnitOfWork
{
    private readonly MovieApiContext _db;
    public IMovieRepository Movies { get; }

    public IActorRepository Actors { get; }

    public IReviewRepository Reviews { get; }

    public UnitOfWork (MovieApiContext db)
    {     
        _db = db;
        Movies = new MovieRepository(db);
        Actors = new ActorRepository(db);
        Reviews = new ReviewRepository(db);
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }


}
