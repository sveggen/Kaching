using Kaching.Services;
using Kaching.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaching.Controllers
{
    [Authorize]
    public class TransfersController : Controller
    {
        private readonly ITransferService _transferService;
        private readonly IPersonService _personService;

        public TransfersController(
            ITransferService transferService,
            IPersonService personService)
        {
            _transferService = transferService;
            _personService = personService;
        }

        // GET: Group/4/Transfers
        [Route("Group/{GroupId}/Transfers")]
        public async Task<IActionResult> Index(int groupId)
        {
            var viewModel = await _transferService.GetTransfers(groupId);
            return View(viewModel);
        }

        // GET: Group/4/Transfers/Create
        [Route("Group/{groupId}/Transfers/Create")]
        public async Task<IActionResult> Create(int groupId)  
        {
            try
            {
                RenderSelectListWithoutYourself(groupId);
                await RenderCurrencySelectList();
                return View();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Group/4/Transfers/Create
        [HttpPost("Group/{groupId}/Transfers/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransferCreateVM transferCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentPerson = _personService.GetPersonByUsername(GetCurrentUserName());
                transferCreateViewModel.SenderId = currentPerson.PersonId;

                await _transferService.CreateTransfer(transferCreateViewModel);

                return RedirectToAction("Index");
            }
            return View(transferCreateViewModel);
        }

        private void RenderSelectListWithoutYourself(int groupId)
        {
            var yourself = _personService.GetPersonByUsername(GetCurrentUserName());
            var selectList = new SelectList(_personService.GetPersonsInGroup(groupId),
                "PersonId", "UserName");
            ViewData["PersonId"] = selectList.Where(x => int.Parse(x.Value) != yourself.PersonId);
        }

        private async Task RenderCurrencySelectList()
        {
            var selectList = new SelectList(await _transferService.GetAllCurrencies(),
                "CurrencyId", "Name");
            ViewData["Currencies"] = selectList;
        }

        private string GetCurrentUserName()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = User;
            return currentUser.Identity.Name;
        }
    }
}