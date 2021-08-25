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
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }


        public async Task<ProfileResponseModel> GetProfile(int userId)
        {
            var user = await _userRepository.GetProfile(userId);

            var ProfileResponseModel = new ProfileResponseModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
            return ProfileResponseModel;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetFavoriteMovies(int userId)
        {
            var user = await _userRepository.GetFavoriteMovies(userId);

            var movieCards = new List<MovieCardResponseModel>();

            foreach (var movie in user.Favorites)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl

                });
            }
            return movieCards;
        }

       

        public async Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMovies(int userId)
        {
            var user = await _userRepository.GetPurchasedMovies(userId);

            var movieCards = new List<MovieCardResponseModel>();

            foreach (var movie in user.Purchases)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl

                });
            }
            return movieCards;
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
                DateOfBirth = model.DateOfBirth,
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

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            var userResponseModel = new UserResponseModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth
            };
            return userResponseModel;
        }

        public async Task<IEnumerable<UserResponseModel>> GetAllUsers()
        {
            var users = await _userRepository.ListAllAsync();
            var userList = new List<UserResponseModel>();
            foreach (var user in users)
            {
                userList.Add(new UserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth
                });
            }
            return userList;
        }

        public async Task<PurchaseMovieResponseModel> PurchaseMovie(PurchaseMovieModel purchaseMovie)
        {
            var dbPurchase = await _purchaseRepository.GetExistsAsync(p => p.MovieId == purchaseMovie.MovieId && p.UserId == purchaseMovie.UserId);

            if (dbPurchase == true)
            {
                throw new ConflictException("You already bought this movie");
            }
            await _purchaseRepository.AddAsync(new Purchase
            {
                UserId = purchaseMovie.UserId,
                MovieId = purchaseMovie.MovieId

            });
            var purchase = new PurchaseMovieResponseModel()
            {
                MovieId = purchaseMovie.MovieId,
                UserId = purchaseMovie.MovieId,
            };
            return purchase;
        }

        public async Task<List<UserPurchaseModel>> GetPurchaseById(int id)
        {
            var user = await _userRepository.GetUserPurchaseById(id);

            var UserPurchaseMovies = new List<UserPurchaseModel>();

            foreach (var u in user.Purchases)
            {
                UserPurchaseMovies.Add(new UserPurchaseModel
                {
                    PurchaseId = u.Id,
                    UserId = u.UserId,
                    MovieId = u.MovieId,
                });
            }
            return UserPurchaseMovies;
        }


        public async Task<FavoriteResponseModel> AddToFavorite(FavoriteRequestModel model)
        {
            var dbFavorite = await _favoriteRepository.GetExistsAsync(f => f.MovieId == model.MovieId && f.UserId == model.UserId);
            if (dbFavorite == true)
            {
                throw new ConflictException("The movie arleady added!");
            }

            var favorite = new Favorite 
            { 
                MovieId = model.MovieId, 
                UserId = model.UserId 
            };
            var createdFavorite = await _favoriteRepository.AddAsync(favorite);
            var favoriteResponse = new FavoriteResponseModel 
            { 
                Id = createdFavorite.Id, 
                MovieId = createdFavorite.MovieId, 
                UserId = createdFavorite.UserId 
            };
            return favoriteResponse;


            //var favorite = await _favoriteRepository.AddAsync(new Favorite
            //{
            //    UserId = model.UserId,
            //    MovieId = model.MovieId
            //});

            //var addFavoriteResonse = new FavoriteResponseModel
            //{
                
            //    UserId = favorite.UserId,
            //    MovieId = favorite.MovieId
            //};

            //return addFavoriteResonse;
        }

        public async Task<UnFavoriteResponseModel> removefromFavorite(UnFavoriteRequestModel model)
        {
            var dbFavorite = await _favoriteRepository.GetExistsAsync(f => f.Id == model.Id);

            if (dbFavorite != true)
            {
                throw new ConflictException("The movie dose not exists!");
            }

            await _favoriteRepository.DeleteAsync(new Favorite
            {
                Id = model.Id,
                UserId = model.UserId,
                MovieId = model.MovieId
            });

            var deleteFavoriteResonse = new UnFavoriteResponseModel
            {
                Id = model.Id,
                UserId = model.UserId,
                MovieId = model.MovieId
            };

            return deleteFavoriteResonse;
        }

        public async Task<List<UserFavoriteMoviesModel>> GetFavoriteById(int id)
        {
            var user = await _userRepository.GetUserFavoriteById(id);

            var UserFavoriteMovies = new List<UserFavoriteMoviesModel>();

            foreach (var m in user.Favorites)
            {
                UserFavoriteMovies.Add(new UserFavoriteMoviesModel
                {
                    Id = m.Id,
                    UserId = m.UserId,
                    MoiveName = m.Movie.Title,
                    MovieId = m.MovieId
                });
            }
            return UserFavoriteMovies;
        }

        public async Task<MovieCardResponseModel> GetFavoriteMovieDetail(int id, int movieId)
        {
            var dbFavorite = await _favoriteRepository.GetFavorite(id, movieId);
            if (dbFavorite == null)
            {
                var movie = new MovieCardResponseModel();
                return movie; 
               // throw new Exception("favourite Moviedetail not found");
            }
            var movieCardResponseModel = new MovieCardResponseModel
            {
                Id = dbFavorite.Id,
                Title = dbFavorite.Movie.Title,
                PosterUrl = dbFavorite.Movie.PosterUrl,
                Budget = (decimal)dbFavorite.Movie.Budget

            };
            return movieCardResponseModel;
        }

        public async Task<List<MovieReviewsModel>> GetReviews(int id)
        {
            var user = await _userRepository.GetReviewsById(id);
            var movieReview = new List<MovieReviewsModel>();

            foreach (var m in user.Reviews)
            {
                movieReview.Add(new MovieReviewsModel
                {
                    Rating = m.Rating,
                    ReviewText = m.ReviewText,
                    MovieId = m.MovieId,
                    UserId = m.UserId
                });
            }
            return movieReview;
        }

        public async Task<ReviewsResponseModel> PostReviews(ReviewsRequestModel model)
        {
            var dbReviews = await _reviewRepository.GetExistsAsync(f => f.MovieId == model.MovieId && f.UserId == model.UserId);

            if (dbReviews != false)
            {
                throw new ConflictException("You already posted a review for this movie");
            }
            await _reviewRepository.AddAsync(new Review
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                Rating = model.Rating,
                ReviewText = model.ReviewText
            });

            var postReviewsResponse = new ReviewsResponseModel
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                Rating = model.Rating,
                ReviewText = model.ReviewText

            };

            return postReviewsResponse;
        }

        public async Task<ReviewsResponseModel> PutReviews(ReviewsRequestModel model)
        {
            var dbReviews = await _reviewRepository.GetExistsAsync(f => f.MovieId == model.MovieId && f.UserId == model.UserId);

            if (dbReviews != true)
            {
                throw new ConflictException("Conflict");
            }
            await _reviewRepository.UpdateAsync(new Review
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                Rating = model.Rating,
                ReviewText = model.ReviewText
            });

            var postReviewsResponse = new ReviewsResponseModel
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                Rating = model.Rating,
                ReviewText = model.ReviewText

            };

            return postReviewsResponse;
        }

        public async Task<string> DeleteReviews(int id, int movieId)
        {
            var dbreview = await _reviewRepository.GetExistsAsync(r => r.UserId == id && r.MovieId == movieId);

            if (dbreview != true)
            {
                throw new ConflictException("Conflict");
            }

            await _reviewRepository.DeleteAsync(new Review
            {
                MovieId = movieId,
                UserId = id
            });
            return "The reviews is Deleted";

        }
    }
}
