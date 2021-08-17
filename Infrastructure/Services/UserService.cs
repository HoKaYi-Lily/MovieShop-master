using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserLoginResponseModel> Login(LoginRequestModel model)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser == null)
            {
                //no user match that email
                return null;
            }

            //get the hashed password and salt from database

            var hashedPassword = GetHashedPassword(model.Password, dbUser.Salt);

            if (hashedPassword == dbUser.HashedPassword)
            {
                //success
                var userLoginResponseModel = new UserLoginResponseModel 
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    Email = dbUser.Email
                };
                return userLoginResponseModel;
            }
            return null;
            //or through an exception later
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel model)
        {
            //make sure with the user entered email does not exists in database.
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser != null)
            {
                //use already has email
                throw new ConflictException("Email already exists");
            }
            //user does not exists in the database

            //generate a unique salt 

            var salt = GenerateSalt();
            var hashedPassword = GetHashedPassword(model.Password, salt);
            //hash password with salt
            //save the salt and hased password to the database.

            //create user entity object 
            var user = new User 
            {
                Email = model.Email, 
                FirstName = model.FirstName,
                LastName = model.LastName, 
                Salt = salt,
                HashedPassword = hashedPassword
            };
            var createdUser = await _userRepository.AddAsync(user);
            //response model
            var userRegisterResponseModel = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            return userRegisterResponseModel;      
        }

        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            //never ever create your own Hashing algorithms, 
            //always use Industry tried and tested Hashing algorithms, 
            //another popular algorithm Aarogon2
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                        password: password,
                                                                        salt: Convert.FromBase64String(salt),
                                                                        prf: KeyDerivationPrf.HMACSHA512,
                                                                        iterationCount: 10000,
                                                                        numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
