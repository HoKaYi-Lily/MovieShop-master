using ApplicationCore.Models;
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
    public class UserController : ControllerBase
    {
      //  private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;


        public UserController(IUserService userService, IReviewService reviewService)
        {
            _reviewService = reviewService;
            _userService = userService;
        }
        //how does it direct to this page, when they need the user info first? 
        //or they just go to the user page without login?
        [Route("{id:int}/GetAllPurchases")]
        [HttpGet]
        public async Task<IActionResult> GetAllPurchases(int id)
        {
            //[FromBody] int userId
            //var userId = _currentUserService.UserId;
            // id from the cookie and sent that id to UserService to get all his/her movies.
            // Filters
            //how do I make that know they have to login firsts? 
            
            var movieCards = await _userService.GetPurchasedMovies(id);
            // call userservice GetAll Purchases
            return Ok(movieCards);
            //400 return error 500 internal error 200 ok
        }

        [Route("{id:int}/GetFavorites")]
        [HttpGet]
        public async Task<IActionResult> GetFavorites( int id)
        {
           // var userId = _currentUserService.UserId;
            var movieCards = await _userService.GetFavoriteMovies(id);
            return Ok(movieCards);
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> PurchasesMovie([FromBody] PurchaseMovieModel purchaseMovie)
        {
            var movie = await _userService.PurchaseMovie(purchaseMovie);

            if (movie == null)
            {
                return NotFound("Purchase movie filed");
            }
            return Ok(movie);
        }


        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> Favorite([FromBody] FavoriteRequestModel model)
        {
            var favoriteMovieResponse = await _userService.AddToFavorite(model);
            return Ok(favoriteMovieResponse);
        }

        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> UnFavorite([FromBody] UnFavoriteRequestModel model)
        {
            var unfavoriteMovieResponse = await _userService.removefromFavorite(model);
            return Ok(unfavoriteMovieResponse);
        }

        [HttpGet]
        [Route("{id:int}/movie/{movieId:int}/favorite")]
        public async Task<IActionResult> GetFavoriteMovie(int id, int movieId)
        {
            var userPurchases = await _userService.GetFavoriteMovieDetail(id, movieId);
            if (userPurchases == null)
            {
                return NotFound("No movie Found");
            }
            return Ok(userPurchases);
        }

        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> PostReview([FromBody] ReviewsRequestModel model)
        {
            var createdReviews = await _userService.PostReviews(model);
            return Ok(createdReviews);
        }

        [HttpPut]
        [Route("review")]
        public async Task<IActionResult> PutReviews([FromBody] ReviewsRequestModel model)
        {

            var createdReviews = await _userService.PutReviews(model);
            return Ok(createdReviews);
        }

        [HttpDelete]
        [Route("{id:int}/movie/{movieId:int}")]
        public async Task<IActionResult> DeleteReviews(int id, int movieId)
        {

            var status = await _userService.DeleteReviews(id, movieId);
            return Ok(status);
        }

        [HttpGet]
        [Route("{id:int}/purchases")]
        public async Task<IActionResult> GetUserPurchases(int id)
        {
            var userPurchases = await _userService.GetPurchaseById(id);

            if (!userPurchases.Any())
            {
                return NotFound("No movie Found");
            }
            return Ok(userPurchases);
        }

        [HttpGet]
        [Route("{id:int}/favorites")]
        public async Task<IActionResult> GetUserfavorites(int id)
        {
            var userFavorites = await _userService.GetFavoriteById(id);

            if (!userFavorites.Any())
            {
                return NotFound("No user Found");
            }
            return Ok(userFavorites);
        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetUserReview(int id)
        {
            var userReviews = await _userService.GetReviews(id);

            if (!userReviews.Any())
            {
                return NotFound("No user Found");
            }
            return Ok(userReviews);
        }



    }
}
