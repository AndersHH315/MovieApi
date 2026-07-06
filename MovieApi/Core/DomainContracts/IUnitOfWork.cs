using MovieApi.Services.Contracts;

namespace MovieApi.Core.DomainContracts
{
    // Add Gengre and MovieDetails
    public interface IUnitOfWork
    {
        IMovieRepository Movies { get; }
        IReviewRepository Reviews { get; }
        IActorRepository Actors { get; }
        Task SaveAsync();
    }
}
