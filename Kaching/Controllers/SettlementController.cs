using Kaching.Services;
using Kaching.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaching.Controllers;

[Authorize]
[Route("Groups/{groupId}/Settlement")]
public class SettlementController : Controller
{
    private readonly ISettlementService _settlementService;

    public SettlementController(ISettlementService settlementService)
    {
        _settlementService = settlementService;
    }
    
    // GET: Groups/6/Settlement/
    [Route("")]
    public async Task<IActionResult> Index(int groupId)
    {
        var group = await _settlementService.GetAllSettlements(groupId);
        return View(group);
    }
}