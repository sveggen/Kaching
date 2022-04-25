#nullable disable
using System.Globalization;
using AutoMapper;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.ViewModels;
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
        private readonly IMapper _mapper;
        private readonly int _currentMonthNumber;

        public ExpensesController(
            IExpenseRepository expenseRepository,
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _currentMonthNumber = DateTime.Now.Month;
            _expenseRepository = expenseRepository;
            _personRepository = personRepository;
            _mapper = mapper;

        }

        // GET: /
        // GET: Expenses
        // GET: Expenses/March
        [Route("")]
        [Route("Expenses/{month?}")]
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

            var expensesByMonth = await _expenseRepository.GetExpenses(monthNumber);
            var persons = _personRepository.GetAllPersons();
            var sum = _expenseRepository.GetExpenseSum(monthNumber);

            // Total amount / nr. of persons
            var share = sum / persons.Count;

            var personViewModelList = _mapper.Map<List<PersonViewModel>>(persons);

            int index = 0;
            foreach (var person in personViewModelList)
            {
                var sumPersonExpenses = _expenseRepository.GetSumOfPersonExpenses(person.PersonId, monthNumber);
                personViewModelList[index].SumOfExpenses = sumPersonExpenses;
                // For each person: amount paid - share = owes/owed
                personViewModelList[index].OwesOwed = personViewModelList[index].SumOfExpenses - share;

                index++;
            }


            var expenseIndexModel = new ExpenseIndexViewModel
            {
                Sum = sum,
                MonthNumber = monthNumber,
                Year = 2022,
                Expenses = _mapper.Map<List<ExpenseViewModel>>(expensesByMonth),
                Persons = personViewModelList,
                CurrentDate = DateOnly.FromDateTime(DateTime.Now).ToString("dddd, dd. MMMM")
            };

            return View(expenseIndexModel);
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

            return View(_mapper.Map<ExpenseViewModel>(expense));
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
            "Description,BuyerId,PaymentStatus,PaymentType")] ExpenseViewModel expenseViewModel)
        {
            var expense = _mapper.Map<Expense>(expenseViewModel);

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
            return View(expenseViewModel);
        }

        // GET: Expenses/Edit/5
        [Route("Expenses/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var expense = await _expenseRepository.GetExpenseById(id);
                RenderSelectList(expense);
                return View(_mapper.Map<ExpenseViewModel>(expense));
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
        public async Task<IActionResult> Edit(int id, [Bind("ExpenseId,Price,CreatorId,Category," + 
                            "Description,BuyerId,PaymentStatus,PaymentType,Created")] ExpenseViewModel expenseViewModel)
        {
            var expense = _mapper.Map<Expense>(expenseViewModel);

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
            return View(expenseViewModel);
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

            return View(_mapper.Map<ExpenseViewModel>(expense));
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

        // POST: Expenses/Pay/5
        [HttpPost("Expenses/Pay"), ActionName("Pay")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PaymentConfirmed(int ExpenseId)
        {
            var expense = await _expenseRepository.GetExpenseById(ExpenseId);

            var currentUsername = GetCurrentUserName();
            var currentPerson = _personRepository.GetPersonByUserName(currentUsername);
            expense.BuyerId = currentPerson.PersonId;

            _expenseRepository.UpdateExpense(expense);
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
