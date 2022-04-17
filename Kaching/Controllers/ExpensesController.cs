#nullable disable
using System.Globalization;
using Kaching.Models;
using Kaching.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Controllers
{

    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IPersonRepository _personRepository;
        private readonly int _currentMonthNumber;

        public ExpensesController(
            IExpenseRepository expenseRepository,
            IPersonRepository personRepository)
        {
            _currentMonthNumber = DateTime.Now.Month;
            _expenseRepository = expenseRepository;
            _personRepository = personRepository;

        }

        // GET: /
        // GET: Expenses/March
        [Route("")]
        [Route("Expenses/{month?}")]
        public async Task<IActionResult> Index(string? month)
        {
            string monthName;
            int monthNumber;

            if (month != null)
            {
                monthName = month;
                monthNumber = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;
            }
            else
            {
                monthNumber = _currentMonthNumber;
            }

            var expensesByMonth = await _expenseRepository.GetExpenses(monthNumber);
            var persons = _personRepository.GetAllPersons();

            Dictionary<string, decimal> sumOfPersonExpenses = new Dictionary<string, decimal>();

            foreach (var person in persons)
            {
                var expenseSum = _expenseRepository.GetSumOfPersonExpenses(person.PersonId, monthNumber);
                sumOfPersonExpenses.Add(person.ConnectedUserName, expenseSum);
            }

            ViewData["SumOfPersonExpenses"] = sumOfPersonExpenses;
            ViewData["MonthExpenseSum"] = _expenseRepository.GetExpenseSum(monthNumber);

            return View(expensesByMonth);
        }

        // GET: Expenses/Details/5
        [Route("Expenses/Details/{id?}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int valueId = id.Value;

            var expense = await _expenseRepository.GetExpenseById(valueId);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        [Route("Expenses/Create")]
        public IActionResult Create()
        {
            RenderSelectListDefault();
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,Price,Category," +
            "Description,BuyerId,PaymentStatus,PaymentType")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                var currentPerson =
                    _personRepository.GetPersonByUserName(GetCurrentUserName());
                expense.Creator = currentPerson;

                // build Expense table
                _expenseRepository.InsertExpense(expense);
                await _expenseRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            RenderSelectList(expense);
            return View(expense);
        }

        // GET: Expenses/Edit/5
        [Route("Expenses/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _expenseRepository.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }
            RenderSelectList(expense);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Expenses/Edit/{id?}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,Price,CreatorId,Category," +
            "Description,BuyerId,PaymentStatus,PaymentType")] Expense expense)
        {
            if (id != expense.ExpenseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _expenseRepository.UpdateExpense(expense);
                    await _expenseRepository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_expenseRepository.GetExpenseExistence(expense.ExpenseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            RenderSelectList(expense);
            return View(expense);
        }

        // GET: Expenses/Delete/5
        [Route("Expenses/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _expenseRepository.GetExpenseById(id);

            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost("Expenses/Delete/{id?}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _expenseRepository.GetExpenseById(id);

            _expenseRepository.DeleteExpense(expense);
            await _expenseRepository.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private void RenderSelectList(Expense expense)
        {   
            ViewData["PersonId"] = new SelectList(_personRepository.GetAllPersons(),
                "PersonId", "ConnectedUserName", expense.BuyerId);
        }

        private void RenderSelectListDefault()
        {
            var selectedUsername = _personRepository.GetPersonByUserName(GetCurrentUserName()).PersonId;
            //ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["PersonId"] = new SelectList(_personRepository.GetAllPersons(),
                "PersonId", "ConnectedUserName", selectedUsername);
        }

        private string GetCurrentUserName()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            return currentUser.Identity.Name;
        }
    }
}
