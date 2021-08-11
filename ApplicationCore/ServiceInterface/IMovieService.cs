using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterface
{
    public interface IMovieService
    {
        //model calling , pick them from entities
        List<MovieCardResponseModel> GetTopRevenueMovies();
        //contractor interfaces and implementation
    }
}
