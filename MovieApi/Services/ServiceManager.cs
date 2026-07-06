using MovieApi.Core.DomainContracts;
using MovieApi.Services.Contracts;

namespace MovieApi.Services;

public class ServiceManager : IServiceManager
{
    private readonly IUnitOfWork _unit;
    public IMovieService Movies { get; }

    public IReviewService Reviews { get; }

    public IActorService Actors { get; }

    public ServiceManager(IUnitOfWork unit)
    {
        _unit = unit;
        Movies = new MovieService(unit);
        Reviews = new ReviewService(unit);
        Actors = new ActorService(unit);
    }
}
