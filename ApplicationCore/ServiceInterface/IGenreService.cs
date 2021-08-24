using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterface
{
   public interface IGenreService
    {
        Task<IEnumerable<GenreResponseModel>> GetAllGenres();
        Task<IEnumerable<MovieCardResponseModel>> GetAllMovies(int id);
        Task<Genre> GetGenreById(int id);
        //the get genre by id has to be using IAsyncRepository to avoid
        //IAsyncrtepository <genres> does not contain a definition for GetMoviesByGenreId
        //no accessible extension method getmoviebygenerId
        //below addition>
        Task<GenreResponseModel> GetGenreDetails(int id);
    }
}
