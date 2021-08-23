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
        Task<IEnumerable<UserResponseModel>> GetAllUsers();
        //Editprofile, buymovie... etc 
    }
}
