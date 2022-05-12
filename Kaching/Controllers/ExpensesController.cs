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
        private readonly int _currentMonthNumber;
        private readonly List<string> _months;

        public ExpensesController(
            IExpenseService expenseService)
        {
            _currentMonthNumber = DateTime.Now.Month;
            _expenseService = expenseService;
            _months = new List<string> 
            {"January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"};
        }

        // GET: /
        // GET: Expenses/March
        [Route("")]
        [Route("Expenses/{month?}")]
        public async Task<IActionResult> Index(string? month)   
        {
            int monthNumber;

            if (month!= null && _months.Contains(month))
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
            else if (month == null)
            {
                monthNumber = _currentMonthNumber;
            }
            else
            {
                return NotFound();
            }

            var viewModel = await _expenseService.GetExpensesByMonth(monthNumber);

            return View(viewModel);
        }

        // GET: Expenses/Details/5
        [Route("Expenses/Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                int valueId = id.Value;

                var expenseVM = await _expenseService.GetExpense(valueId);
                if (expenseVM == null)
                {
                    return NotFound();
                }

                return View(expenseVM);
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

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseEventCreateViewModel expenseEventCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentPerson = 1;
                expenseEventCreateViewModel.CreatorId = currentPerson;

                // build Expense table
                await _expenseService.CreateExpense(expenseEventCreateViewModel);

                return RedirectToAction(nameof(Index));
            }
            return View(expenseEventCreateViewModel);
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/CreateRecurring")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRecurring(ExpenseEventCreateViewModel expenseEventCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentPerson = 1;
                expenseEventCreateViewModel.CreatorId = currentPerson;

                await _expenseService.CreateExpense(expenseEventCreateViewModel);

                return RedirectToAction(nameof(Index));
            }
            //RenderSelectList(expenseEventCreateViewModel);
            return View(expenseEventCreateViewModel);
        }

        // GET: Expenses/Edit/5
        [Route("Expenses/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var expenseVM = await _expenseService.GetExpense(id);
                RenderSelectList(expenseVM);
                return View(expenseVM);
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
        public async Task<IActionResult> Edit(int id, ExpenseEventViewModel expenseEventViewModel)
        {

            if (id != expenseEventViewModel.ExpenseEventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _expenseService.UpdateExpense(expenseEventViewModel);
                return RedirectToAction(nameof(Index));
            }
            RenderSelectList(expenseEventViewModel);
            return View(expenseEventViewModel);
        }

        // POST: Expenses/EditAll/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/EditAll/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAll(int id, ExpenseEventViewModel expenseEventViewModel)
        {

            if (id != expenseEventViewModel.ExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _expenseService.UpdateRecurringExpenses(expenseEventViewModel);
                return RedirectToAction(nameof(Index));
            }
            RenderSelectList(expenseEventViewModel);
            return View(expenseEventViewModel);
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

        // POST: Expenses/Delete/5
        [HttpPost("Expenses/DeleteRecurring/{id?}"), ActionName("Delete")]
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

        private void RenderSelectList(ExpenseEventViewModel expenseEventViewModel)
        {   
            ViewData["PersonId"] = new SelectList(_expenseService.GetPersons(),
                "PersonId", "ConnectedUserName", expenseEventViewModel.BuyerId);
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
