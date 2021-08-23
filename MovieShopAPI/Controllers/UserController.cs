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
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;

        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
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
        
    }
}
