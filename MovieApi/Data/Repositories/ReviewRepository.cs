using Microsoft.EntityFrameworkCore;
using MovieApi.Core.DomainContracts;
using MovieApi.Core.Models;
using MovieApi.Services.Contracts;

namespace MovieApi.Data.Repositories;

public class ReviewRepository(MovieApiContext db) : IReviewRepository
{
    private readonly MovieApiContext _db = db;
    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        var reviews = await _db.Reviews.ToListAsync();
        return reviews;
    }
    public async Task<Review?> GetReviewAsync(int id)
    {
        var review = await _db.Reviews.FindAsync(id);

        if (review == null)
            return null;
        return review;
    }

    public async Task<IEnumerable<Review>> GetReviewsByMovieId(int id)
    {
        var review = await _db.Reviews
            .Where(r => r.MovieId == id)
            .ToListAsync();
        return review;
    }
    public void Add(Review review)
    {
        _db.Reviews.Add(review);
    }
    public void Update(Review review)
    {
        _db.Reviews.Update(review);
    }
    public void Remove(Review review)
    {
        _db.Reviews.Remove(review);
    }

    public async Task<bool> CheckAmountOfReviews(int movieId)
    {
        var movie = await _db.Movies
            .Include(m => m.Reviews)
            .FirstOrDefaultAsync(m => m.Id == movieId);
        if (movie == null)
            throw new Exception("Movie not found");
        if (movie.Reviews.Count >= 10)
            throw new Exception("Movies can only have up to 10 reviews");
        if (movie.Year < DateTime.Now.AddYears(-20))
        {
           if (movie.Reviews.Count >= 5)
                throw new Exception("Cannot add more than 5 reviews for a movie older than 20 years");
        }

        return true;
    }
}
