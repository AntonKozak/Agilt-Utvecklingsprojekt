using Microsoft.AspNetCore.Mvc;

namespace agilt_projekt.web.Controllers;

    [Route("admin")]
    public class AdminEventController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
