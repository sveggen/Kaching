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
        [Route("Expenses/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var expenseVM = await _expenseService.GetExpense(id);

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
        public async Task<IActionResult> Create(ExpenseCreateVM expenseCreateVM)
        {
            if (ModelState.IsValid)
            {
                expenseCreateVM.CreatorId = _personService.GetPersonByUsername(GetCurrentUserName()).PersonId;
                await _expenseService.CreateExpense(expenseCreateVM);

                return RedirectToAction(nameof(Index));
            }
            return View(expenseCreateVM);
        }

        // POST: Expenses/CreateRecurring
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/CreateRecurring")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRecurring(ExpenseCreateRecurringVM expenseCreateRecurringVM)
        {
            if (ModelState.IsValid)
            {
                expenseCreateRecurringVM.CreatorId = _personService.GetPersonByUsername(GetCurrentUserName()).PersonId;
                await _expenseService.CreateExpense(expenseCreateRecurringVM);

                return RedirectToAction(nameof(Index));
            }
            return View(expenseCreateRecurringVM);
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

        // GET: Expenses/EditRecurring/5
        [Route("Expenses/EditRecurring/{id}")]
        public async Task<IActionResult> EditRecurring(int id)
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
        public async Task<IActionResult> Edit(int id, ExpenseEditVm expenseEditVM)
        {
            if (ModelState.IsValid)
            {
                await _expenseService.UpdateExpense(expenseEditVM);
                return RedirectToAction(nameof(Index));
            }
           // RenderSelectList(expenseEventViewModel);
            return NotFound();
        }

        // POST: Expenses/EditRecurring/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/EditRecurring/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecurring(int id, ExpenseEditRecurringVm expenseEditRecurringVM)
        {
            if (ModelState.IsValid)
            {
                await _expenseService.UpdateExpense(expenseEditRecurringVM);
                return RedirectToAction(nameof(Index));
            }
            //var expenseVM = await _expenseService.GetExpense()

            //RenderSelectList(expenseEventViewModel);
            //return View(expenseCreateRecurringVM);
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

        // GET: Expenses/Delete/5
        [Route("Expenses/DeleteRecurring/{id}")]
        public async Task<IActionResult> DeleteRecurring(int id)
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
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            return currentUser.Identity.Name;
        }
    }
}