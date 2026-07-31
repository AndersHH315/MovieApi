using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Core.DomainContracts;
using MovieApi.Core.DTOs;
using MovieApi.Core.Models;
using MovieApi.Core.Paging;
using MovieApi.Services.Contracts;

namespace MovieApi.Services;

// Use DTO's here! Move them from controller to here and do db calls through repositories!
public class ReviewService(IUnitOfWork unit) : IReviewService
{
    private readonly IUnitOfWork _unit = unit;

    public async Task<PagedResult<ReviewDto>> GetReviewsAsync(PagingParameters paging)
    {
        var reviews = await _unit.Reviews.GetAllAsync();
        return await reviews.Select(r => new ReviewDto
        {
            ReviewerName = r.ReviewerName,
            Comment = r.Comment,
            Rating = r.Rating
        }).AsQueryable().ToPagedResult(paging.CurrentPage, paging.PageSize);
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsForSpecificMovieAsync(int movieid)
    {
        var movieReview = await _unit.Reviews.GetReviewsByMovieId(movieid);
        return movieReview.Select(r => new ReviewDto
        {
            ReviewerName = r.ReviewerName,
            Comment = r.Comment,
            Rating = r.Rating
        });
    }

    public async Task<ReviewDto?> PostReviewAsync(int movieid, ReviewDto reviewDto)
    {
        var movie = await _unit.Movies.GetMovieAsync(movieid);

        if (movie == null)
            return null;

        var review = new Review
        {
            ReviewerName = reviewDto.ReviewerName,
            Comment = reviewDto.Comment,
            Rating = reviewDto.Rating,
            MovieId = movieid
        };

        _unit.Reviews.Add(review);
        await _unit.SaveAsync();

        var newReview = new ReviewDto
        {
            ReviewerName = review.ReviewerName,
            Comment = review.Comment,
            Rating = review.Rating
        };

        return newReview;
    }

    public async Task<Review?> DeleteReviewAsync(int id)
    {
        var review = await _unit.Reviews.GetReviewAsync(id);
        if (review == null)
            return null;

        _unit.Reviews.Remove(review);
        await _unit.SaveAsync();

        return review;
    }
}
