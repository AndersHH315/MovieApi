using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.DTOs;
using MovieApi.Interfaces;
using MovieApi.Models;

namespace MovieApi.Services
{
    public class ReviewService(IMovieApiContext db) : IReviewService
    {
        private readonly IMovieApiContext _db = db;

        public async Task<IEnumerable<ReviewDto?>?> GetReviewsAsync()
        {
            var reviews = await _db.Reviews.Select(r => new ReviewDto
            {
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating
            }).ToListAsync();

            if (reviews.Count == 0)
                return null;

            return reviews;
        }

        public async Task<IEnumerable<Review>> GetReviewsForSpecificMovieAsync(int movieid)
        {
            var movieReview = await _db.Reviews.Where(r => r.MovieId == movieid).Select(r => new Review
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                Comment = r.Comment,
                Rating = r.Rating,
                MovieId = r.MovieId
            }).ToListAsync();

            return movieReview;
        }

        public async Task<Review?> PostReviewAsync(int movieid, ReviewDto reviewDto)
        {
            var movie = await _db.Movies.FindAsync(movieid);

            if (movie == null)
                return null;

            var review = new Review
            {
                ReviewerName = reviewDto.ReviewerName,
                Comment = reviewDto.Comment,
                Rating = reviewDto.Rating,
                MovieId = (int)movieid
            };
            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();

            return review;
        }

        public async Task<Review?> DeleteReviewAsync(int id)
        {
            var review = await _db.Reviews.FindAsync(id);
            if (review == null)
                return null;

            _db.Reviews.Remove(review);
            await _db.SaveChangesAsync();

            return review;
        }
    }
}
