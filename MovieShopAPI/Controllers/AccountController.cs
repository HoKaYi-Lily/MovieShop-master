﻿using ApplicationCore.Models;
using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestModel model)
        {
            var user = await _userService.RegisterUser(model);
            return Ok(user);
            
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("No User Found");
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

            return Ok(user);
          //  return Ok(new { token = GenerateJWT(loginUser));
        }

        //private object GenerateJWT(UserLoginResponseModel loginUser)
        //{
        //    throw new NotImplementedException();
        //    //or do we have better ways?
        //    //references:https://github.com/GraceL2020/MovieShop/blob/master/MovieShop.API/Controllers/AccountController.cs
        //    //https://github.com/shauna2020w/MovieShop/blob/main/MovieShop/MovieShop.API/Controllers/AccountController.cs

        //}
    }
}