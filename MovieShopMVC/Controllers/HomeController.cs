using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using ApplicationCore.ServiceInterface;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
       //private readonly ILogger<HomeController> _logger;

        private IMovieService _movieService;
        //here call the movie service
        // public HomeController(ILogger<HomeController> logger)
        //this parameter pass in any instance that implementing this interface 
        public HomeController(IMovieService movieService)
        {
            //_logger = logger;
            _movieService = movieService;
            //supply actual implementation to this controller with inject
        }

        public async Task<IActionResult> Index()
        {
            //movieservice returning a task, so we should change this to task from  public IActionResult Index()
            var movieCards = await _movieService.GetTopRevenueMovies();
            return View(movieCards);
            //run this and should show the actual data from the database
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


//clean architecture

//dependency injection (learn tmr)
//interface : dont have any implementation, just have construct, the defintion
//one class can not inherit multiple class, but can inherit many interface and one class

//application core layer
//entities: C# classes that represents your domain/databases objects 
//models:C# objects
//entities and models different
//20 columns , movies table
//entity => Movie 20 properties, represent backend database table

//Models => UI, create model based on the UI implement, based on business UI preference data, that want to pass to mvc layers
//Movie List =>MovieCardModel =>id, title, posterimage
//their properties , DTO (Data Transfer objects)=>API
//models depends on databases, models as much as your clients need per page

//var movies = _movieService.GetTopRevenueMovies();
////check defintion of method => go to interface
//ViewBag.PageTitle = "Top Revenue Movies";
////viewbag is dynamic type, any naming properties convention is okay
//ViewData["TotalMovies"] = movies.Count();
//return View(movies);
////get top revenue movie and display on the view
//// 3 ways to pass data from controller to view
//// 1 Strongly Typed Models ******
//// 2 ViewBag
//// 3 ViewData
//// get top revenue movie and display on the view
//// localhost:5001/movies/details/2
///
//create a poster view that can be reuse in moviecard, show allmovie, or userfavorite, genre etc -partial view that can be reuse in multiple places
//In MVC => every view inherit the layout View from shared folder
