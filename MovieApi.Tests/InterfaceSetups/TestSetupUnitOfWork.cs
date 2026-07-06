using Microsoft.EntityFrameworkCore;
using MovieApi.Core.DomainContracts;
using MovieApi.Data;
using MovieApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Tests.InterfaceSetups
{
    public class TestSetupUnitOfWork : IUnitOfWork
    {
        private readonly MovieApiContext _context;
        public IActorRepository Actors { get; }
        public IMovieRepository Movies { get; }

        public IReviewRepository Reviews { get; }

        public TestSetupUnitOfWork(MovieApiContext context)
        {
            _context = context;

            Movies = new MovieRepository(context);
            Actors = new ActorRepository(context);
            Reviews = new ReviewRepository(context);
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
