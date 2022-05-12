using Kaching.Services;
using Kaching.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaching.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IExpenseService _expenseService;

        public PaymentsController(
            IPaymentService paymentService,
            IExpenseService expenseService)
        {
            _paymentService = paymentService;
            _expenseService = expenseService;
        }

        // GET: Payments/
        [Route("Payments/")]
        public async Task<IActionResult> Index(string? month)
        {
            var viewModel = await _paymentService.GetPayments();

            return View(viewModel);
        }

        // GET: Payments/Create
        [Route("Payments/Create")]
        public IActionResult Create()  
        {
            RenderSelectListDefault();
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Payments/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentCreateViewModel paymentCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentPerson = _expenseService.GetPersonByUsername(GetCurrentUserName());
                paymentCreateViewModel.SenderId = currentPerson.PersonId;

                // build Expense table
                await _paymentService.CreatePayment(paymentCreateViewModel);

                return RedirectToAction(nameof(Index));
            }
            //RenderSelectList(expenseEventCreateViewModel);
            return View(paymentCreateViewModel);
        }

        private void RenderSelectListDefault()
        {
            ViewData["PersonId"] = new SelectList(_expenseService.GetPersons(),
                "PersonId", "ConnectedUserName");
        }

        private string GetCurrentUserName()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            return currentUser.Identity.Name;
        }
    }
}
