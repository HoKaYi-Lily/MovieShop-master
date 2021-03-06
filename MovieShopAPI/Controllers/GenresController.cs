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
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetAllGenres();

            if (genres == null)
            {
                return NotFound("No genre Found!");
            }
            return Ok(genres);
        }

        //[HttpGet]
        //[Route("Index")]
        //public async Task<IActionResult> Index()
        //{
        //    var genres = await _genreService.GetAllGenres();
        //    return Ok(genres);
        //}
    }
}
