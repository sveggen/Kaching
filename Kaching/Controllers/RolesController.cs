using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Controllers;

[Authorize(Roles = "Administrator")]
[Route("Admin/Roles")]
public class RolesController : Controller
{

    private RoleManager<IdentityRole> _roleManager;
    
    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    // GET: Admin/Roles/
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }

    // GET: Admin/Roles/Create
    [Route("Create")]
    public async Task<IActionResult> Create()
    {
        return View(new IdentityRole());
    }
    
    // POST: Admin/Roles/Create
    [Route("Create")]
    [HttpPost]
    public async Task<IActionResult> Create(IdentityRole role)
    {
        await _roleManager.CreateAsync(role);
        return RedirectToAction("Index");
    }
}