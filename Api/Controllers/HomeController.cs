using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class HomeController : Controller
    {

        [Route("api")]
        [HttpGet]
        public IActionResult Index()
        {
            return Json(new { id=1 });
        }
    }
}