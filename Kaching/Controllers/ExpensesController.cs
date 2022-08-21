#nullable disable
using System.Globalization;
using Kaching.Services;
using Kaching.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaching.Controllers
{
    [Authorize] 
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;
        private readonly IPersonService _personService;
        private readonly int _currentMonthNumber;
        private readonly List<string> _months;

        public ExpensesController(
            IExpenseService expenseService,
            IPersonService personService)
        {
            _currentMonthNumber = DateTime.Now.Month;
            _expenseService = expenseService;
            _personService = personService;
            _months = new List<string> 
            {"January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"};
        }

        // GET: /
        // GET: Expenses/March
        [Route("")]
        [Route("Expenses/{month?}/{year?}")]
        public async Task<IActionResult> Index(string? month, string? year)
        {
            int monthNumber;

            if (month!= null && _months.Contains(month) && year != null)
            {
                try
                {
                    var monthName = month;
                    monthNumber = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return NotFound();
                }
            }
            else if (month == null && year == null)
            {
                monthNumber = _currentMonthNumber;
                year = DateTime.Now.Year.ToString();
            }
            else
            {
                return NotFound();
            }

            var viewModel = await _expenseService.GetExpensesByMonth(monthNumber, year);

            return View(viewModel);
        }

        // GET: Expenses/Details/5
        [Route("Expenses/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var expenseVm = await _expenseService.GetExpense(id);

                return View(expenseVm);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Expenses/Create
        [Route("Expenses/Create")]
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

        // GET: Expenses/CreateRecurring
        [Route("Expenses/CreateRecurring")]
        public IActionResult CreateRecurring()
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

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCreateVm expenseCreateVm)
        {
            if (ModelState.IsValid)
            {
                expenseCreateVm.CreatorId = _personService.GetPersonByUsername(GetCurrentUserName()).PersonId;
                await _expenseService.CreateExpense(expenseCreateVm);

                return RedirectToAction(nameof(Index));
            }
            return View(expenseCreateVm);
        }

        // GET: Expenses/Edit/5
        [Route("Expenses/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var expenseVm = await _expenseService.GetExpense(id);
                RenderSelectList(expenseVm);
                return View(expenseVm);
            }   
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Expenses/EditRecurring/5
        [Route("Expenses/EditRecurring/{id}")]
        public async Task<IActionResult> EditRecurring(int id)
        {
            try
            {
                var expenseVm = await _expenseService.GetExpense(id);
                RenderSelectList(expenseVm);
                return View(expenseVm);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/Edit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseEditVm expenseEditVm)
        {
            if (ModelState.IsValid)
            {
                await _expenseService.UpdateExpense(expenseEditVm);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: Expenses/Delete/5
        [Route("Expenses/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var expenseVm = await _expenseService.GetExpense(id);
                return View(expenseVm);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Expenses/Delete/5
        [HttpPost("Expenses/Delete/{id?}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _expenseService.DeleteExpense(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Expenses/DeleteRecurring/12
        [HttpPost("Expenses/DeleteRecurring/{id?}"), ActionName("DeleteRecurring")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecurringConfirmed(int id)
        {
            try
            {
                await _expenseService.DeleteRecurringExpense(id);
                return RedirectToAction(nameof(Index));
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

        private void RenderSelectList(ExpenseVm expenseEventViewModel)
        {   
            ViewData["PersonId"] = new SelectList(_personService.GetPersons(),
                "PersonId", "ConnectedUserName", expenseEventViewModel.ResponsibleId);
        }

        private void RenderSelectListDefault()
        {
            var selectedUsername = _personService.GetPersonByUsername(GetCurrentUserName()).PersonId;
            ViewData["PersonId"] = new SelectList(_personService.GetPersons(),
                "PersonId", "ConnectedUserName", selectedUsername);
        }

        private string GetCurrentUserName()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = User;
            return currentUser.Identity.Name;
        }
    }
}