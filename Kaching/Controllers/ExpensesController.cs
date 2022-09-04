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
        
        // GET: Groups/3/Expenses/March/2022
        [Route("Groups/{groupId}/Expenses/{month}/{year}")]
        public async Task<IActionResult> Index(int groupId, string month, string year)
        {
            int monthNumber;

            if (month != null && year != null 
                              && _dateHelper.StringIsMonth(month) 
                              && _dateHelper.StringIsYear(year))
            {
                monthNumber = _dateHelper.GetMonthNumber(month);
            }
            else
            {
                return NotFound();
            }

            var viewModel = await _expenseService.GetExpensesByMonth
                (monthNumber, Int32.Parse(year), groupId);
            
            ViewData["group"] = groupId;
            ViewData["monthNumber"] = monthNumber;
            
            return View(viewModel);
        }
        
        // GET: Groups/3/Expenses/
        [Route("Groups/{groupId}/Expenses/")]
        public async Task<IActionResult> Index(int groupId)
        {
            var monthNumber = _dateHelper.GetCurrentMonthNumber();
            var year = _dateHelper.GetCurrentYear();
            
            var viewModel = await _expenseService.GetExpensesByMonth
                (monthNumber, Int32.Parse(year), groupId);

            ViewData["group"] = groupId;
            ViewData["monthNumber"] = monthNumber;
            
            return View(viewModel);
        }

        // GET: Groups/4/Expenses/Details/5
        [Route("Groups/{groupId}/Expenses/Details/{expenseId}")]
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

        // GET: Groups/7/Expenses/CreateRecurring
        [Route("Groups/{groupId}/Expenses/CreateRecurring")]
        public IActionResult CreateRecurring(int groupId)
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
        
        // POST: Groups/7/Expenses/7/CreateRecurring
        [HttpPost("Groups/{groupId}/Expenses/CreateRecurring")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRecurring(ExpenseCreateVm expenseCreateVm, int groupId)
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

        // GET: Groups/7/Expenses/Edit/5
        [Route("Groups/{groupId}/Expenses/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, int groupId)
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
        [Route("Groups/{groupId}/Expenses/EditRecurring/{id}")]
        public async Task<IActionResult> EditRecurring(int groupId, int expenseId)
        {
            try
            {
                var expenseVm = await _expenseService.GetExpense(expenseId);
                RenderSelectList(expenseVm);
                RenderCategorySelectList();
                return View(expenseVm);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Groups/6/Expenses/Edit/5
        [HttpPost("Groups/{groupId}/Expenses/Edit/{id?}")]
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
        
        // POST: Expenses/Pay/
        [HttpPost("/Expenses/Pay/"), ActionName("Pay")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayConfirmed(int expenseId)
        {
            try
            {
                var buyerId = _personService.GetPersonByUsername
                        (GetCurrentUserName()).PersonId;
                await _expenseService.PayExpense(expenseId, buyerId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Expenses/Delete/
        [HttpPost("/Expenses/Delete/"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int expenseId)
        {
            try
            {
                await _expenseService.DeleteExpense(expenseId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST: Expenses/DeleteRecurring/
        [HttpPost("/Expenses/DeleteRecurring/"), ActionName("DeleteRecurring")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRecurringConfirmed(int expenseId)
        {
            await _expenseService.DeleteRecurringExpense(expenseId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Expenses/PersonalExpenses
        [Route("/Expenses/PersonalExpenses")]
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

        // GET: Groups/7/Settlement
        [Route("/Groups/{groupId}/Settlement")]
        public IActionResult Settlement()
        {
            return View();
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