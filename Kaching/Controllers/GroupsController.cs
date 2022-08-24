﻿using Kaching.Services;
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