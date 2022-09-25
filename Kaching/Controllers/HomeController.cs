using Microsoft.AspNetCore.Mvc;

namespace Kaching.Controllers;

public class HomeController : Controller
{
    // GET: /
    [Route("Home/")]
    public IActionResult Index()
    {
        return View();
    }
}