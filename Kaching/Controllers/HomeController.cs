using Microsoft.AspNetCore.Mvc;

namespace Kaching.Controllers;

[Route("Home")]
public class HomeController : Controller
{
    // GET: /
    [Route("")]
    public IActionResult Index()
    {
        return View();
    }
}