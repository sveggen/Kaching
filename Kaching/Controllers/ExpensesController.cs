﻿#nullable disable
using Kaching.Helpers;
using Kaching.Services;
using Kaching.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kaching.Controllers
{
    [Authorize]
    [Route("Groups/{groupId}/Expenses")]
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
        [Route("{month}/{year}")]
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
        [Route("")]
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
        [Route("Details/{expenseId}")]
        public async Task<IActionResult> Details(int groupId, int expenseId)
        {
            try
            {
                var expenseVm = await _expenseService.GetExpense(expenseId);

                return View(expenseVm);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // GET: Groups/7/Expenses/7/Create
        [Route("Create")]
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
        [Route("CreateRecurring")]
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
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCreateVm expenseCreateVm, int groupId)
        {
            if (ModelState.IsValid)
            {
                expenseCreateVm.CreatorId = _personService.GetPersonByUsername(GetCurrentUserName()).PersonId;
                expenseCreateVm.GroupId = groupId;
                await _expenseService.CreateExpense(expenseCreateVm);

                var year = expenseCreateVm.DueDate.Year;
                var month = expenseCreateVm.DueDate.Month;
                
                var redirect = await RedirectToExpenses(expenseCreateVm.GroupId, month, year);
                return redirect;
            }

            return View(expenseCreateVm);
        }
        
        // POST: Groups/7/Expenses/7/CreateRecurring
        [HttpPost("CreateRecurring")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRecurring(ExpenseCreateVm expenseCreateVm, int groupId)
        {
            if (ModelState.IsValid)
            {
                expenseCreateVm.CreatorId = _personService.GetPersonByUsername(GetCurrentUserName()).PersonId;
                expenseCreateVm.GroupId = groupId;
                await _expenseService.CreateExpense(expenseCreateVm);
                
                var year = expenseCreateVm.DueDate.Year;
                var month = expenseCreateVm.DueDate.Month;

                var redirect = await RedirectToExpenses(expenseCreateVm.GroupId, month, year);
                return redirect;
            }

            return View(expenseCreateVm);
        }

        // GET: Groups/7/Expenses/Edit/5
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, int groupId)
        {
            try
            {
                var expenseVm = await _expenseService.GetExpense(id);
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
        [HttpPost("Edit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseEditVm expenseEditVm)
        {
            if (!ModelState.IsValid) return NotFound();
            if (Request.Form["recurring"] == "on")
            {
                await _expenseService.UpdateRecurringExpenses(expenseEditVm);
            }
            else
            {
                await _expenseService.UpdateExpense(expenseEditVm);
            }
            var redirect = await RedirectToExpensesFromExpense(expenseEditVm.ExpenseId);
            return redirect;
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
                
                var redirect = await RedirectToExpensesFromExpense(expenseId);
                await _expenseService.PayExpense(expenseId, buyerId);
                
                return redirect;
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
                var redirect = await RedirectToExpensesFromExpense(expenseId);
                await _expenseService.DeleteExpense(expenseId);
                return redirect;
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
            var redirect = await RedirectToExpensesFromExpense(expenseId);
            await _expenseService.DeleteRecurringExpense(expenseId);
            return redirect;
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

        private async Task<RedirectToActionResult> RedirectToExpensesFromExpense(int expenseId)
        {
            var expense = await _expenseService.GetExpense(expenseId);
            var groupId = expense.GroupId;
            var expenseYear = expense.DueDate.Year;
            var expenseMonth = _dateHelper.GetMonthName(expense.DueDate.Month);
            
            return RedirectToAction("Index", new { groupId, year = expenseYear, month = expenseMonth} );
        }
        
        private async Task<RedirectToActionResult> RedirectToExpenses(int groupId, int month, int year)
        {
            var monthName = _dateHelper.GetMonthName(month);
            return RedirectToAction("Index", new { groupId, year, month = monthName} );
        }
    }
}