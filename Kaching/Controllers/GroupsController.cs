using Kaching.Services;
using Kaching.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Administrator")]
    [Route("Groups/")]
    public IActionResult Index()
    {
        var groups = _groupService.GetGroups();
        return View(groups);
    }
    
    // GET: /
    // GET: Groups/
    [Route("/")]
    [Route("MyGroups/")]
    public IActionResult MyGroups()
    {
        try
        {
            var personId = _personService.GetPersonByUsername(
                GetCurrentUserName()).PersonId;
            var groups = _groupService.GetPersonsGroups(personId);
            return View(groups);
        }
        catch
        {
            return NotFound();   
        }

    }
    
    /*// GET: Groups/4
    [Route("Groups/{groupId}")]
    public IActionResult SelectedGroup(int groupId)
    {
        Response.Cookies.Append("group-id", groupId.ToString(), SetCookieOptions());
        
        return View();
    }*/
    
    

    // GET: Groups/Create
    [Authorize(Roles = "Administrator")]
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
    [Authorize(Roles = "Administrator")]
    [HttpPost("Groups/Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GroupCreateVm groupCreateVm)
    {
        if (ModelState.IsValid)
        {
            groupCreateVm.Personal = false;
            _groupService.AddGroup(groupCreateVm);

            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    
    
    // POST: Groups/Delete
    [HttpPost("Groups/Delete"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int groupId)
    {
        _groupService.DeleteGroup(groupId);
        return RedirectToAction(nameof(Index));
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
    
    private CookieOptions SetCookieOptions()
    {
        var cookie = new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(1)
        };
        return cookie;
    }
}