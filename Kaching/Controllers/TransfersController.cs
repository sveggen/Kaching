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

        // GET: Transfers/
        [Route("Transfers/")]
        public async Task<IActionResult> Index(string? month)
        {
            try
            {
                var viewModel = await _transferService.GetTransfers();
                return View(viewModel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Transfers/Create
        [Route("Transfers/Create")]
        public IActionResult Create()  
        {
            try
            {
                RenderSelectListDefault();
                return View();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Transfers/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransferCreateVM transferCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentPerson = _personService.GetPersonByUsername(GetCurrentUserName());
                transferCreateViewModel.SenderId = currentPerson.PersonId;

                await _transferService.CreateTransfer(transferCreateViewModel);

                return RedirectToAction(nameof(Index));
            }
            //RenderSelectList(expenseEventCreateViewModel);
            return View(transferCreateViewModel);
        }

        private void RenderSelectListDefault()
        {
            ViewData["PersonId"] = new SelectList(_personService.GetPersons(),
                "PersonId", "ConnectedUserName");
        }

        private string GetCurrentUserName()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = User;
            return currentUser.Identity.Name;
        }
    }
}