using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
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

        public async Task<MovieCardResponseModel> CreateMovie(MovieCreateRequestModel movie)
        {
            var newMovie = await _movieRepository.AddAsync(new Movie
            {
                Title = movie.Title,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
            });

            return new MovieCardResponseModel
            {
                Id = newMovie.Id,
                Budget = newMovie.Budget.GetValueOrDefault(),
                PosterUrl = newMovie.PosterUrl,
                Title = newMovie.Title,

            };
        }



        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            var movieDetailsModel = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                Budget = movie.Budget,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Revenue = movie.Revenue,
                Price = movie.Price,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                OriginalLanguage = movie.OriginalLanguage,

            };
            movieDetailsModel.Casts = new List<CastResponseModel>();
            foreach (var cast in movie.MovieCasts)
            {
                movieDetailsModel.Casts.Add(new CastResponseModel { Id = cast.CastId, Name = cast.Cast.Name, Character = cast.Character, Gender = cast.Cast.Gender, ProfilePath = cast.Cast.ProfilePath });
            }

            movieDetailsModel.Genres = new List<GenreResponseModel>();
            foreach (var genre in movie.Genres)
            {
                movieDetailsModel.Genres.Add(new GenreResponseModel { Id = genre.Id, Name = genre.Name });
            }
            return movieDetailsModel;

        }

        public async Task<List<MovieReviewsModel>> GetMovieReviews(int id)
        {
            var movie = await _movieRepository.GetMovieReviews(id);

            var movieReviews = new List<MovieReviewsModel>();
            foreach (var review in movie.Reviews)
            {
                movieReviews.Add(new MovieReviewsModel { UserId = review.UserId, ReviewText = review.ReviewText, Rating = review.Rating });
            }

            return movieReviews;
        }

        public async Task<List<MovieCardResponseModel>> GetTopRatingMovies()
        {
            var dbMovies = await _movieRepository.GetTopRatedMovies();

            var movies = new List<MovieCardResponseModel>();
            foreach (var movie in dbMovies)
            {
                movies.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Budget = movie.Budget.GetValueOrDefault(),
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title,
                    Rating = movie.Rating,
                });
            }
            return movies;
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


        public async Task<MovieDetailsResponseModel> UpdateMovie(MovieUpdateRequestModel movie)
        {
            var dbMovie = await _movieRepository.GetByIdAsync(movie.Id);
            if (dbMovie == null)
            {
                throw new ConflictException("No movie exists");
            }

            dbMovie.Title = movie.Title;
            dbMovie.PosterUrl = movie.PosterUrl;
            dbMovie.BackdropUrl = movie.BackdropUrl;
            dbMovie.Overview = movie.Overview;
            dbMovie.Tagline = movie.Tagline;
            dbMovie.Budget = movie.Budget;
            dbMovie.Revenue = movie.Revenue;
            dbMovie.ImdbUrl = movie.ImdbUrl;
            dbMovie.TmdbUrl = movie.TmdbUrl;
            dbMovie.ReleaseDate = movie.ReleaseDate;
            dbMovie.RunTime = movie.RunTime;
            dbMovie.Price = movie.Price;

            var updatedDbMovie = await _movieRepository.UpdateAsync(dbMovie);

            return await GetMovieDetails(updatedDbMovie.Id);
        }


        //----
        public async Task<List<MovieCardResponseModel>> GetfilterGenres(int id)
        {
            var movies = await _movieRepository.GetMoviesByGenre(id);

            var movieCards = new List<MovieCardResponseModel>();

            foreach (var movie in movies)
            {
                foreach (var genre in movie.Genres)
                {
                    if (genre.Id == id)
                    {
                        movieCards.Add(new MovieCardResponseModel { Id = movie.Id, Budget = movie.Budget.GetValueOrDefault(), PosterUrl = movie.PosterUrl, Title = movie.Title });
                    }
                }
            }

            return movieCards;

        }
        //---
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