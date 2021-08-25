using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    //attribute routing
    [Route("api/[controller]")]
  
    public class MoviesController : ControllerBase
    {
        //controller base has view and all those things, dont need controller class's view,just need json data stuff from controller base class 
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            if (!movies.Any())
            {
                NotFound("No movies exist");
            }
            return Ok(movies);
        }


        // api/movies/toprevenue
        [Route("toprevenue")]
        [HttpGet]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            // 200 OK
            return Ok(movies);
            // Serialization => object to another type of object
            // C# to JASON
            //C# to XML using XMLSerilizer
            // DeSerialization => JSON to C#
            // .NET Core 3.1 or less, JSON.NET => 3rd party library, included
            // System.Text.Json =>
            // along with data you also need to return HTTP status code
        }


        [HttpGet]
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _movieService.GetMovieDetails(id);
            return Ok(movieDetails);
        }


        [HttpGet]
        [Route("genre/{id:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int id)
        {
            var movie = await _movieService.GetfilterGenres(id);

            if (movie == null)
            {
                return NotFound($"No Movie Found for that {id}");
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);

            if (movie == null)
            {
                return NotFound($"No Movie Found for that {id}");
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatingMovies()
        {
            var movies = await _movieService.GetTopRatingMovies();

            if (!movies.Any())
            {
                return NotFound("No Movie Found");
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReview(int id)
        {
            var movie = await _movieService.GetMovieReviews(id);

            if (movie == null)
            {
                return NotFound("No Movies Found");
            }

            return Ok(movie);

        }

    }
}
