using Kaching.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kaching.Controllers;

[Authorize]
public class SettlementController : Controller
{
    private readonly ISettlementService _settlementService;

    public SettlementController(ISettlementService settlementService)
    {
        _settlementService = settlementService;
    }
    
    // GET: Groups/6/Settlement/
    [Route("Groups/{groupId}/Settlement/")]
    public IActionResult Index(int groupId)
    {
        var groups = _settlementService.GetAllSettlements(groupId);
        return View(groups);
    }
    
    // GET: Groups/7/Settlement/May/2022
    [Route("Groups/{groupId}/Settlement/{monthNumber}/{year}")]
    public async Task<IActionResult> Settlement(int groupId, int monthNumber, int year)
    {
        var group = await _settlementService.GetSettlement(monthNumber, year, groupId);
        return View(group);
    }
}