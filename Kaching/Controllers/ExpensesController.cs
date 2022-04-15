﻿#nullable disable
using System.Globalization;
using System.Threading.Tasks.Dataflow;
using Kaching.Data;
using Kaching.Models;
using Kaching.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Controllers
{

    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly DataContext _context;
        private readonly IExpenseRepository _expenseRepository;
        private readonly int _currentMonthNumber;

        public ExpensesController(
            DataContext context,
            IExpenseRepository expenseRepository)
        {
            _context = context;
            _currentMonthNumber = DateTime.Now.Month;
            _expenseRepository = expenseRepository;

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

            ViewData["MonthExpenseSum"] = _expenseRepository.GetExpenseSum(monthNumber);


            return View(expensesByMonth);
        }

        // GET: Expenses/Details/5
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
                var currentPerson = GetPersonByUserName();
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
        [Route("Expenses/Edit/{id?}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense.FindAsync(id);
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
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.ExpenseId))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .Include(e => e.Creator)
                .FirstOrDefaultAsync(m => m.ExpenseId == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expense.FindAsync(id);
            _context.Expense.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expense.Any(e => e.ExpenseId == id);
        }

        private void RenderSelectList(Expense expense)
        {   
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "ConnectedUserName", expense.BuyerId);
        }

        private void RenderSelectList()
        {
            //ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "ConnectedUserName");
        }

        private void RenderSelectListDefault()
        {
            var selectedUsername = GetPersonByUserName().PersonId;
            //ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "ConnectedUserName", selectedUsername);
        }

        private string GetCurrentUserName()
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            return currentUser.Identity.Name;
        }

        private Person GetPersonByUserName()
        {
            return _context.Person
            .FirstOrDefault(p => p.ConnectedUserName == GetCurrentUserName());
        }
    }
}
