using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewsResponseModel> PostReviews(ReviewsRequestModel model)
        {
            var dbReviews = await _reviewRepository.GetExistsAsync(f => f.MovieId == model.MovieId && f.UserId == model.UserId);

            if (dbReviews != false)
            {
                throw new ConflictException("You already posted a review for this movie");
            }
            await _reviewRepository.AddAsync(new Review
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                Rating = model.Rating,
                ReviewText = model.ReviewText
            });

            var postReviewsResponse = new ReviewsResponseModel
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                Rating = model.Rating,
                ReviewText = model.ReviewText

            };

            return postReviewsResponse;
        }
    }
}
