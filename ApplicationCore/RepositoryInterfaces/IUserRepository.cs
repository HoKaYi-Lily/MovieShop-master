using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository: IAsyncRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        ////---
        Task<User> GetFavoriteMovies(int id);
        Task<User> GetPurchasedMovies(int id);
        ////---
        //getting the user record, and registration log in, get the one single record of the user , 

        Task<User> GetUserPurchaseById(int id);
        Task<User> GetUserFavoriteById(int id);
        Task<User> GetReviewsById(int id);
        Task<User> GetProfile(int id);
        //Task<User> GetAccountbyId(int id);

        //Task<User> GetAccount();
        //Task<User> EditProfile();

        //Task<User> BuyMovie();

       // Task<User> FavoriteMovie();
    
    
    }
}
