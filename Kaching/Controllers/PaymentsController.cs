using System.Globalization;
using Kaching.MailViewModels;
using Kaching.Repositories;
using Kaching.Services;
using Kaching.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaching.Controllers
{
    public class PaymentsController : Controller
    {

        private readonly IEmailService _emailService;
        private readonly IPaymentService _paymentService;
        private readonly IExpenseService _expenseService;
        private readonly int _currentMonthNumber;

        public PaymentsController(IEmailService emailService,
            IPaymentService paymentService,
            IExpenseService expenseService)
        {
            _emailService = emailService;
            _paymentService = paymentService;
            _expenseService = expenseService;
        }


        // GET: Payments/
        // GET: Payments/March
        [Route("Payments/")]
        [Route("Payments/{month?}")]
        public async Task<IActionResult> Index(string? month)
        {
            string monthName;
            int monthNumber;

            if (month != null)
            {
                try
                {
                    monthName = month;
                    monthNumber = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return NotFound();
                }
            }
            else
            {
                monthNumber = _currentMonthNumber;
            }

            var viewModel = await _paymentService.GetPaymentsByMonth(monthNumber);

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
            var selectedUsername = _expenseService.GetPersonByUsername(GetCurrentUserName()).PersonId;
            ViewData["PersonId"] = new SelectList(_expenseService.GetPersons(),
                "PersonId", "ConnectedUserName", selectedUsername);
        }

        private string GetCurrentUserName()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            return currentUser.Identity.Name;
        }


    }
}
