using Kaching.Services;
using Kaching.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaching.Controllers;

[Route("Groups")]
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
    [Route("")]
    public IActionResult Index()
    {
        var groups = _groupService.GetGroups();
        return View(groups);
    }
    
    // GET: /
    [Route("/")]
    public IActionResult MyGroups()
    {
        try
        {
            if (User.Identity.IsAuthenticated)
            {
                var personId = _personService.GetPersonByUsername(
                    GetCurrentUserName()).PersonId;
                var groups = _groupService.GetPersonsGroups(personId);
                return View(groups);
            }

            return RedirectToAction("Index", "Home");
        }
        catch
        {
            return NotFound();   
        }

    }

    // GET: Groups/Create
    [Authorize(Roles = "Administrator")]
    [Route("Create")]
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
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GroupCreateVm groupCreateVm)
    {
        if (ModelState.IsValid)
        {
            var intList = new List<int>();
            intList.Add(1);
            intList.Add(3);
            groupCreateVm.MemberIds = intList;
            groupCreateVm.Personal = false;
            _groupService.AddGroup(groupCreateVm);

            return RedirectToAction(nameof(Index));
        }
        return View();
    }
    
    
    // POST: Groups/Delete
    [HttpPost("Delete"), ActionName("Delete")]
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