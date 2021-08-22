using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
       //what should its route be???
        [HttpGet]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            //throw new Exception("some exception happened");
            //movieservice returning a task, so we should change this to task from  public IActionResult Index()
            var movieCards = await _movieService.GetTopRevenueMovies();
            return Ok(movieCards);
            //run this and should show the actual data from the database
        }

    }
}
