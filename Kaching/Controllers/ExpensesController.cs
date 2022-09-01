#nullable disable
using Kaching.Helpers;
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
        private readonly DateHelper _dateHelper; 

        public ExpensesController(
            IExpenseService expenseService,
            IPersonService personService)
        {
            _expenseService = expenseService;
            _personService = personService;
            _dateHelper = new DateHelper();
        }

        // GET: Groups/3/Expenses/
        // GET: Groups/3/Expenses/March/2022
        [Route("Groups/{groupId}/Expenses/{month?}/{year?}")]
        public async Task<IActionResult> Index(int groupId, string? month, string? year)
        {
            int monthNumber;

            if (month != null 
                && year != null 
                && _dateHelper.StringIsMonth(month) 
                && _dateHelper.StringIsYear(year))
            {
                monthNumber = _dateHelper.GetMonthNumber(month);
            }
            else if (month == null || year == null)
            {
                monthNumber = _dateHelper.GetCurrentMonthNumber();
                year = _dateHelper.GetCurrentYear();
            }
            else
            {
                return NotFound();
            }

            var viewModel = await _expenseService.GetExpensesByMonth
                (monthNumber, Int32.Parse(year), groupId);
            ViewData["group"] = groupId;
            
            return View(viewModel);
        }

        // GET: Groups/4/Expenses/Details/5
        [Route("Group/{groupId}/Expenses/Details/{expenseId}")]
        public async Task<IActionResult> Details(int groupId, int expenseId)
        {
            try
            {
                // MISSING: check if user is part of group
                
                var expenseVm = await _expenseService.GetExpense(expenseId);

                return View(expenseVm);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Groups/7/Expenses/7/Create
        [Route("Groups/{groupId}/Expenses/Create")]
        public IActionResult Create(int groupId)
        {
            try
            {
                RenderSelectListDefault();
                RenderCategorySelectList();
                return View();
            }
            catch (Exception)
            {
                return NotFound(); 
            }
        }

        // GET: Expenses/CreateRecurring
        [Route("Groups/{groupId}/Expenses/CreateRecurring")]
        public IActionResult CreateRecurring()
        {
            try
            {
                RenderSelectListDefault();
                RenderCategorySelectList();
                return View();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Groups/7/Expenses/7/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Groups/{groupId}/Expenses/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCreateVm expenseCreateVm, int groupId)
        {
            if (ModelState.IsValid)
            {
                expenseCreateVm.CreatorId = _personService.GetPersonByUsername(GetCurrentUserName()).PersonId;
                expenseCreateVm.GroupId = groupId;
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

        // GET: Expenses/Personal
        [Route("/Expenses/Personal")]
        public async Task<IActionResult> PersonalIndex()
        {
            var year = DateTime.Now.Year;
            var month = _dateHelper.GetCurrentMonthNumber();

            var person =
                _personService.GetPersonByUsername(GetCurrentUserName());

            try
            {
                var expensesVm =
                    await _expenseService.GetPersonalExpensesByMonth(month, year, person.PersonId);
                return View(expensesVm);
            }
            catch (Exception)
            {
                return View();
            }
        }

        private void RenderCategorySelectList()
        {
            ViewData["Category"] = new SelectList(
                _expenseService.GetCategories(),
                "CategoryId", "Name");
        }

        private void RenderSelectList(ExpenseVm expenseViewModel)
        {
            ViewData["PersonId"] = new SelectList(_personService.GetPersons(),
                "PersonId", "UserName", expenseViewModel.ResponsibleId);
        }

        private void RenderSelectListDefault()
        {
            var selectedUsername = _personService.GetPersonByUsername(GetCurrentUserName()).PersonId;
            ViewData["PersonId"] = new SelectList(_personService.GetPersons(),
                "PersonId", "UserName", selectedUsername);
        }

        private string GetCurrentUserName()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = User;
            return currentUser.Identity.Name;
        }

    }
}