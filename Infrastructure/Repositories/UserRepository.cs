using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        //generate constructor then implementation
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> GetFavoriteMovies(int userId)
        {
            var user = await _dbContext.Users.Include(u => u.Favorites).ThenInclude(u => u.Movie)
                .FirstOrDefaultAsync(u => u.Id == userId);
            //it connect to user>favourite>movie then use userId what>>?? why how, do they locate the userid first, or connect all graph first
            return user;

        }

        public async Task<User> GetPurchasedMovies(int userId)
        {
            var user = await _dbContext.Users.Include(u => u.Purchases).ThenInclude(u => u.Movie)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return user;
            // throw new NotImplementedException();
        }

        //public async Task<User> GetAccountbyId(int userId)
        //{
        //    var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        //    return user;
        //}

        public async Task<User> GetProfile(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<User> GetUserFavoriteById(int id)
        {
            var user = await _dbContext.Users.Include(u => u.Favorites).ThenInclude(u => u.Movie).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> GetUserPurchaseById(int id)
        {
            var user = await _dbContext.Users.Include(u => u.Purchases).ThenInclude(u => u.Movie).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User> GetReviewsById(int id)
        {
            var user = await _dbContext.Users.Include(u => u.Reviews).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        //public Task<User> EditProfile()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<User> BuyMovie()
        //{
        //    throw new NotImplementedException();
        //    //add movie to purchased..
        //}

        //public Task<User> GetAccount()
        //{
        //    throw new NotImplementedException();
        //    //we are going to use EfRepository 
        //}
    }
}
