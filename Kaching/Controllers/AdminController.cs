#nullable disable
using Kaching.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaching.Controllers;

[Authorize(Roles = "Administrator")]
[Route("Admin")]
public class AdminController : Controller
{

    private readonly IPersonService _personService;

    public AdminController(IPersonService personService)
    {
        _personService = personService;
    }
    
    // GET: Admin/
    [Route("")]
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    
    // GET: Admin/Users
    [Route("Users")]
    public async Task<IActionResult> UsersIndex()
    {
        var persons = _personService.GetPersons();
        return View(persons);
    }
    
    
    
    
}