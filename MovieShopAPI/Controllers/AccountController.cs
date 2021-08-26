using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AccountController(IUserService userService, IUserRepository userRepository, IConfiguration configuration)
        {
            _userService = userService;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                NotFound($"Account dosen't exist for id: {id}");
            }
            return Ok(user);
        }

        [HttpGet]
        [Route("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
            {
                NotFound("No users exist");
            }
            return Ok(users);
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

            //do we still need the stuff above???

            // Generate the JWT
            return Ok(
                new
                {
                    token = GenerateJwt(user)
                });
            //generate JWT token
            //return Ok(user);
            //  return Ok(new { token = GenerateJWT(loginUser));
        }

        private string GenerateJwt(UserLoginResponseModel user)
        {
            var claims = new List<Claim>
            {
                new Claim( ClaimTypes.NameIdentifier, user.Id.ToString() ),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            // Create JWT

            // get the secret key from appsettings or Azure Key/Vault

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecretKey"]));
            // select the hashing algo

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // get the expiration time
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("ExpirationHours"));

            // create the JWT token with above claims and credentials and expiration time

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Issuer"],
                Audience = _configuration["Audience"],
                Subject = identityClaims,
                Expires = expires,
                SigningCredentials = credentials
            };

            var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(encodedJwt);
            // Store Application Secrets in Azure Key/Vault  
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
