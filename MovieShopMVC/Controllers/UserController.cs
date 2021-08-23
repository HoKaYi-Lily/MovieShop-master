using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;

        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }


        //user/GetAllPurchase

    
        public async Task<IActionResult> GetAllPurchases()
        {
            var userId = _currentUserService.UserId;
            // id from the cookie and sent that id to UserService to get all his/her movies.
            // Filters
          
            var movieCards = await _userService.GetPurchasedMovies(userId);
            // call userservice GetAll Purchases
            return View(movieCards);
        }

        
        public async Task<IActionResult> GetFavorites()
        {
             var userId = _currentUserService.UserId;
            var movieCards = await _userService.GetFavoriteMovies(userId);
            return View(movieCards);
        }


        public async Task<IActionResult> GetProfile()
        {
            var userId = _currentUserService.UserId;
            var user = await _userService.GetProfile(userId);
            return View(user);
        }



        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        
        public async Task<IActionResult> BuyMovie()
        {
            return View();
        }

      
        public async Task<IActionResult> FavoriteMovie()
        {
            return View();
        }
    }
}

