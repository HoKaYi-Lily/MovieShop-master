using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterface
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel model);

        Task<UserLoginResponseModel> Login(LoginRequestModel model);
        Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMovies(int userId);
        Task<IEnumerable<MovieCardResponseModel>> GetFavoriteMovies(int userId);

       // Task<ProfileResponseModel> GetAccountbyId(int userId);

     //   Task<IEnumerable<ProfileResponseModel>> GetAccount();
        Task<ProfileResponseModel> GetProfile(int userId);
        // Task<IEnumerable<UserResponseModel>> GetAllUsers();
        //Editprofile, buymovie... etc 

        Task<UserResponseModel> GetUserById(int id);
        Task<IEnumerable<UserResponseModel>> GetAllUsers();
        Task<PurchaseMovieResponseModel> PurchaseMovie(PurchaseMovieModel purchaseMovie);
        Task<List<UserPurchaseModel>> GetPurchaseById(int id);
        Task<FavoriteResponseModel> AddToFavorite(FavoriteRequestModel model);
        Task<UnFavoriteResponseModel> removefromFavorite(UnFavoriteRequestModel model);
        Task<List<UserFavoriteMoviesModel>> GetFavoriteById(int id);
        Task<MovieCardResponseModel> GetFavoriteMovieDetail(int id, int movieId);
        Task<List<MovieReviewsModel>> GetReviews(int id);
        Task<ReviewsResponseModel> PostReviews(ReviewsRequestModel model);
        Task<ReviewsResponseModel> PutReviews(ReviewsRequestModel model);
        Task<string> DeleteReviews(int id, int movieId);
    }
}
