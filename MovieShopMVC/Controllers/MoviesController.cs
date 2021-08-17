using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieServices;

        public MoviesController(IMovieService movieServices)
        {
            _movieServices = movieServices;
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _movieServices.GetMovieDetails(id);
            //call the database and get movie details by id
            return View(movieDetails);
        }
    }
}

//finish this method and cast one
//go to movieservices and send them the movie details servicdes
//both the moviedetail and cast table, script screenshot