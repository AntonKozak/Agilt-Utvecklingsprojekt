using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace agilt_projekt.web.Controllers;

public class HomeController : Controller
{
    
    public IActionResult Index()
    {
        // ViewBag.Message= "Veckans erbjudande";
        return View("Index");
    }  

    [Route("registration")]
    public IActionResult Registration()
    {
        return View("Registration");
    }  
}
