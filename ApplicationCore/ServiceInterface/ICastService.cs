using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterface
{
    public interface ICastService
    {
        Task<CastResponseModel> GetCastDetails(int id);
        Task<CastResponseModel> GetCastById(int id);
    }
}
