using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ClientMVC.Controllers
{
    [Authorize]
    public class ApiCallController : Controller
    {
        public IActionResult Index()
        {
            var token = HttpContext.GetTokenAsync("access_token");
            ViewBag.token = token.Result;

            return View();
        }

      
    }
}