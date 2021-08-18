using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        //DI remember constructor dependency injection

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }
        public async Task<CastResponseModel> GetCastDetails(int id)
        {
            //get the return from repository that is entity and put it in our model

            var cast = await _castRepository.GetByIdAsync(id);

            var castResponsesModel = new CastResponseModel
            {
                Id = cast.Id,
                Name = cast.Name,
                Gender = cast.Gender,
                ProfilePath = cast.ProfilePath,
            };

            castResponsesModel.Movies = new List<MovieDetailResponseModel>();

            foreach (var movie in cast.MovieCasts)
            {
                castResponsesModel.Movies.Add(new MovieDetailResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }

            return castResponsesModel;

        }
    }
}
