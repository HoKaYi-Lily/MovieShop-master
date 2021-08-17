using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterface;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model) {

            if (!ModelState.IsValid) 
            {
                return View();
            } 

            var user = await _userService.Login(model);

            if (user == null)
            {
                throw new Exception("Invalid Login");
            }
            //cookie based authertication...
            return LocalRedirect("~/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)      
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //see if it is valid, other wise
            var registeredUser = await _userService.RegisterUser(model);
            return RedirectToAction("Login");

            //call the service and repository to hash the password with salt and save to database
           
        }

        //the parameter can put case insenstive stuff ex: string firstName, string FIRSTNAME, string email, string em, string LASTNAME
    }
}
//why is keys not id , names are not unqiue, usually we need to make sure name is unique, its http feature 
//look into the input, use the name as a key, mvc just taking the poster information, that's how http play
//showing some btn or hiding some btn
// public IActionResult Register(UserRegisterRequestModel model, string firstName, string FIRSTNAME, string email, string em, string LASTNAME)
//we look at the model to see what it can save and pass