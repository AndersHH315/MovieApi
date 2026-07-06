using MovieApi.Core.DomainContracts;

namespace MovieApi.Services.Contracts;

public interface IServiceManager
{
    IMovieService Movies { get; }
    IReviewService Reviews { get; }
    IActorService Actors { get; }
}
