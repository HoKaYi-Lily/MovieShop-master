using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterface
{
    public interface IMovieService
    {
        //model calling , pick them from entities
        Task<List<MovieCardResponseModel>> GetTopRevenueMovies();
        //contractor interfaces and implementation

        Task<MovieDetailsResponseModel> GetMovieDetails(int id);
        Task<List<MovieCardResponseModel>> GetAllMovies();
        Task<MovieCardResponseModel> CreateMovie(MovieCreateRequestModel movie);
        Task<MovieDetailsResponseModel> UpdateMovie(MovieUpdateRequestModel movie);
        Task<List<MovieCardResponseModel>> GetfilterGenres(int id);
        Task<List<MovieCardResponseModel>> GetTopRatingMovies();
        Task<List<MovieReviewsModel>> GetMovieReviews(int id);
    }
}
 