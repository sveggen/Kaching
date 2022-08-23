using Kaching.Services;
using Kaching.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaching.Controllers;

public class GroupsController : Controller
{
    private readonly IGroupService _groupService;
    private readonly IPersonService _personService;
    
    public GroupsController(IGroupService groupService,
        IPersonService personService)
    {
        _groupService = groupService;
        _personService = personService;
    }
    
    // GET: Groups/
    [Route("Groups/")]
    public IActionResult Index()
    {
        return View();
    }
    
    
    // GET: Groups/Create
    [Route("Groups/Create")]
    public IActionResult Create()  
    {
        try
        {
            RenderSelectListWithoutYourself();
            return View();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
    
    // POST: Groups/Create
    [HttpPost("Groups/Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GroupCreateVm groupCreateVm)
    {
        if (ModelState.IsValid)
        {
            _groupService.AddGroup(groupCreateVm);

            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    
    private void RenderSelectListWithoutYourself()
    {
        var yourself = _personService.GetPersonByUsername(GetCurrentUserName());
        var selectList = new SelectList(_personService.GetPersons(),
            "PersonId", "UserName");
        ViewData["PersonId"] = selectList.Where(x => int.Parse(x.Value) != yourself.GetHashCode());
    }

    private string GetCurrentUserName()
    {
        System.Security.Claims.ClaimsPrincipal currentUser = User;
        return currentUser.Identity.Name;
    }
}