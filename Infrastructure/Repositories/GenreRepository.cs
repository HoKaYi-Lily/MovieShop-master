using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenreRepository : EfRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        //public override async Task<Genre> GetByIdAsync(int id)
        //{
        //    var genre = await _dbContext.Genres.Where(g => g.Id == id).Include(g => g.MovieGenres)
        //                               .ThenInclude(g => g.Movies).FirstOrDefaultAsync();
        //    return genre;

        //}
    }
}
