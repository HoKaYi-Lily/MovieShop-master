using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterface;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            //giving the getted instance to the private_movierepository variable with DI's help
        }
        public async Task<List<MovieCardResponseModel>> GetTopRevenueMovies()
        {
            //call repositories and get the real data from database
            //call the movie repository class
            var movies = await _movieRepository.Get30HighestRevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();
            //highestrevenue is async method, we should await it
            //the return type movies is a type of entity, but the task return type is a model
            foreach (var movie in movies)
            { //looping inside the object and putting inside that moviecards
                movieCards.Add(new MovieCardResponseModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }
            //so we are sepreately putting entity and model for different backend and frontend usage??
            return movieCards;
        }

        //public getdetails() {}
    }
}

//Iasyncrepository > Imoverepository>efrepository>movierepository>imovieservices>movieservice
//// 3 ways to pass data from controller to view
//// 1 Strongly Typed Models ******
//// 2 ViewBag
//// 3 ViewData
//// get top revenue movie and display on the view
//// localhost:5001/movies/details/2
//// call repositories and get the real data from database
//var movies = new List<MovieCardResponseModel>()
//{
//              new MovieCardResponseModel {Id = 1, Title = "Avengers: Infinity War"},
//              new MovieCardResponseModel {Id = 2, Title = "Avatar"},
//              new MovieCardResponseModel {Id = 3, Title = "Star Wars: The Force Awakens"},
//              new MovieCardResponseModel {Id = 4, Title = "Titanic"},
//              new MovieCardResponseModel {Id = 5, Title = "Inception"},
//              new MovieCardResponseModel {Id = 6, Title = "Avengers: Age of Ultron"},
//              new MovieCardResponseModel {Id = 7, Title = "Interstellar"},
//              new MovieCardResponseModel {Id = 8, Title = "Fight Club"},
//              new MovieCardResponseModel {Id = 9, Title = "The Lord of the Rings: The Fellowship of the Ring" }


//};

//return movies;