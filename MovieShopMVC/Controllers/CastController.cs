
using ApplicationCore.ServiceInterface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class CastController : Controller
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var details = await _castService.GetCastDetails(id);
            return View(details);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

//maybe we do not need this