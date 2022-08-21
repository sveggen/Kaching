#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaching.Controllers;

[Authorize(Roles = "Administrator")]

public class AdminController : Controller
{
    // GET: Admin/
    [Route("Admin/")]
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    
    
}