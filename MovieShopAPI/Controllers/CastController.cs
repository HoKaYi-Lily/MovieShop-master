using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastController : ControllerBase
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }
      
        //[HttpGet]
        //[Route("Details/{id}")]
        //public async Task<IActionResult> Details(int id)
        //{
        //    var details = await _castService.GetCastDetails(id);
        //    return Ok(details);
        //}

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCast(int id)
        {
            var cast = await _castService.GetCastById(id);

            if (cast == null)
            {
                return NotFound($"No cast Found for that {id}");
            }
            return Ok(cast);
        }

    }
}
