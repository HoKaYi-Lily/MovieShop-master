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

        public override async Task<Movie> GetByIdAsync(int Id)
        
        {
            //return base.GetByIdAsync(Id);
            //movie table, then genres, then casts and rating 
            //include () movies, then include () the navigation methods and properties

            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == Id);
            
            //var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
            // .Include(m =>m.MovieGenres).ThenInclude(m => m.Genres)
           // .FirstOrDefaultAsync(m => m.Id == Id);
            //we are using include property to join the navigation property, also using the reviews for avreaging and return movie
            //do the same for cast table, cast repository , cast services 


            if (movie == null)
            {
                throw new Exception($"No Movie Found for the id {Id}");
            }

            var movieRating = await _dbContext.Reviews.Where(m => m.MovieId == Id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            //how to I make it start from index 1

            movie.Rating = movieRating;
            return movie;

            //if the r - reviewing is null then assign it a 0, other take the average
            //default if empty because some movie might not have review , left join in sql
                //because we only need one movie information
                //why do we need movie cast and cast, 
                //because moviecast only gonna get us movie id and cast id, 
                //but we also need cast name , because movie cast has cast navigation properties
                //the movies here is from DbSet<Movie> Movies from Db context
                //from that inside the movie entity -> we have icollection access to other table (those navigator property)
        }
    }
}
