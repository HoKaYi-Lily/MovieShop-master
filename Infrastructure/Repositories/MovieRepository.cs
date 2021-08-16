using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
           
        }
        //9 method ^^ to 1 method, because the EfRepository implemented all the base method
        public async Task<IEnumerable<Movie>> Get30HighestRevenueMovies()
        {
        
            //get 30 movies from movie table order by revenue
            //select top 30 from movies order by reveues;
            //the movies is getted from the db set
            //ToList(), count().  or we can loop through
            //I/O bound operation
            //EF has methods that have both asyn and non - async ones


            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            //loop the webpage
            //the _dbcontext is in dbclass, protected
            return movies;
        }
    }
}
