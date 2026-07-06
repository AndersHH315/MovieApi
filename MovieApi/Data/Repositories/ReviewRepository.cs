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
}
