using ApplicationCore.Models;
using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            // Cookie based Authentication
            // Forms Authentication
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userService.Login(model);
            if (user == null)
            {
                throw new Exception("Invalid Login");
            }
            // Cookies based authentication....
            // store some information in the Cookies, Authentication cookie... Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // Identity class... and Principle
            // go to an bar => check your identity => Driving License
            // go to Airport => check your passport
            // Create a Movie => claim with role value as Admin

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create the cookies
            // HttpContext

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

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
            // call the service and repository to hash the password with salt and save to DB
            var registeredUser = await _userService.RegisterUser(model);

            return RedirectToAction("Login");
        }
    }
}

























//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ApplicationCore.Models;
//using ApplicationCore.ServiceInterface;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;

//namespace MovieShopMVC.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly IUserService _userService;

//        public AccountController(IUserService userService)
//        {
//            _userService = userService;
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login(LoginRequestModel model) {

//            if (!ModelState.IsValid) 
//            {
//                return View();
//            } 

//            var user = await _userService.Login(model);

//            if (user == null)
//            {
//                throw new Exception("Invalid Login");
//            }

//            // Cookies based authentication....
//            // store some information in the Cookies, Authentication cookie... Claims
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.GivenName, user.FirstName),
//                new Claim(ClaimTypes.Surname, user.LastName),
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
//            };

//            // Identity class... and Principle
//            // go to an bar => check your identity => Driving License
//            // go to Airport => check your passport
//            // Create a Movie => claim with role value as Admin

//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

//            // create the cookies
//            // HttpContext

//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
//            //cookie based authertication...
//            return LocalRedirect("~/");
//        }

//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(UserRegisterRequestModel model)      
//        {
//            if (!ModelState.IsValid)
//            {
//                return View();
//            }

//            //see if it is valid, other wise
//            var registeredUser = await _userService.RegisterUser(model);
//            return RedirectToAction("Login");

//            //call the service and repository to hash the password with salt and save to database

//        }

//        //the parameter can put case insenstive stuff ex: string firstName, string FIRSTNAME, string email, string em, string LASTNAME
//    }
//}
////why is keys not id , names are not unqiue, usually we need to make sure name is unique, its http feature 
////look into the input, use the name as a key, mvc just taking the poster information, that's how http play
////showing some btn or hiding some btn
//// public IActionResult Register(UserRegisterRequestModel model, string firstName, string FIRSTNAME, string email, string em, string LASTNAME)
////we look at the model to see what it can save and pass