using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientMVC.Controllers
{
    [Authorize]
    public class ApiCallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}