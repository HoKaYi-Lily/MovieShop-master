using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface ICastRepository : IAsyncRepository<Cast>
    {

        // Task<IEnumerable<Cast>> ListAllAsync();
        //we do not have to do list since they already have it?
        //need to find movie, moviecast (movie id, cast id), cast 
        Task getCastDetails(int id);
    }
}
