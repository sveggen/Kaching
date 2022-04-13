#nullable disable
using Kaching.Data;
using Kaching.Models;
using Kaching.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kaching.Controllers
{

    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IExpenseRepository _expenseRepository;
        private readonly int currentMonthNumber;

        public ExpensesController(
            DataContext context,
            UserManager<IdentityUser> userManager,
            IExpenseRepository expenseRepository)
        {
            _context = context;
            _userManager = userManager;
            currentMonthNumber = DateTime.Now.Month;
            _expenseRepository = expenseRepository;

        }

        // GET: Expenses/3
        public async Task<IActionResult> Index(int? id)
        {

            int inputId = currentMonthNumber;

            if (id != null)
            {
                inputId = id.Value;
            }

            var expensesByMonth = await _expenseRepository.GetExpenses(inputId);

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
        public IActionResult Create()
        {
            RenderSelectListDefault();
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExpenseId,Price,Category," +
            "Description,PaymentType,Payer,PayerId,Payer.PaymentStatus,Payer.PersonId")] Expense expense)
        {

            if (ModelState.IsValid)
            {
                var currentPerson = GetPersonByUserName();
                expense.Person = currentPerson;

                // build Payer table
                _context.Payer.Add(expense.Payer);
                await _context.SaveChangesAsync();


                // build Expense table
                _expenseRepository.InsertExpense(expense);
                await _expenseRepository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            RenderSelectList(expense);
            return View(expense);
        }

        // GET: Expenses/Edit/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,Price,PersonId," +
            "Category,Description, PaymentType, PaymentStatus")] Expense expense)
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
                .Include(e => e.Person)
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
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "ConnectedUserName", expense.PersonId);
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
