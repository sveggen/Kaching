using Kaching.Data;
using Kaching.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly DataContext _context;

        public ExpenseRepository(DataContext context)
        {
            _context = context;
        }   

        public void DeleteExpense(Expense expense)
        {
             _context
                .Expense.Remove(expense);
        }

        public async Task<Expense> GetExpenseById(int expenseId)
        {
            return await _context.Expense
                .Include(e => e.BaseExpense)
                .Include(b => b.Buyer)
                .Include(g => g.Group)
                .Include(g => g.BaseExpense.Category)
                .Include(g => g.Currency)
                .FirstOrDefaultAsync(m => m.ExpenseId == expenseId);
        }

        public async Task<List<Expense>> GetExpenses(int monthNumber, string year)
        {
            return await _context.Expense
                .Include(e => e.Buyer)
                .Include(e => e.BaseExpense)
                .Where(p => p.DueDate.Month == monthNumber)
                .Where(p =>p.DueDate.Year == Int16.Parse(year))
                .OrderByDescending(e => e.DueDate)
                .ToListAsync();
        }

        public decimal GetSumExpensesByMonth(int monthNumber, string year)
        {
            return  _context.Expense
                .Include(e => e.Buyer)
                .Include(e => e.BaseExpense)
                .Where(p => p.DueDate.Month == monthNumber)
                .Where(p =>p.DueDate.Year == Int16.Parse(year))
                .Sum(i => i.Price);
        }


        public async Task<List<Expense>> GetPersonExpensesByMonth(int personId, int monthNumber)
        {
            return await _context.Expense
                .Include(e => e.BaseExpense)
                .Where(p => p.DueDate.Month == monthNumber)
                .Where(e => e.BuyerId == personId)
                .ToListAsync();
        }

        public async Task<List<Expense>> GetGroupExpensesByMonth(int monthNumber, int year, int groupId)
        {
            return await _context.Expense
                .Include(e => e.BaseExpense)
                .Include(e => e.Buyer)
                .Include(g => g.Group)
                .Include(g => g.BaseExpense.Category)
                .Include(g => g.Currency)
                .Where(p => p.DueDate.Year == year)
                .Where(p => p.DueDate.Month == monthNumber)
                .Where(p => p.GroupId == groupId)
                .OrderByDescending(e => e.DueDate)
                .ToListAsync();
        }

        public decimal GetSumOfPersonExpensesByMonth(int personId, int monthNumber)
        {
            return _context.Expense
                .Include(v => v.BaseExpense)
                .Include(b => b.Buyer)
                .Where(p => p.DueDate.Month == monthNumber)
                .Where(e => e.BuyerId == personId)
                .Sum(e => e.Price);
        }

        public async Task<List<Expense>> GetGroupExpenses(int groupId)
        {
            return await _context.Expense
                .Include(e => e.BaseExpense)
                .Include(e => e.Buyer)
                .Include(g => g.Group)
                .Include(g => g.BaseExpense.Category)
                .Include(g => g.Currency)
                .Where(p => p.GroupId == groupId)
                .OrderByDescending(e => e.DueDate)
                .ToListAsync();
        }

        public void InsertExpense(Expense expense)
        {
            _context.Add(expense);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateExpense(Expense expense)
        {
            _context.Update(expense);
        }

        public bool GetExpenseExistence(int id)
        {
            return _context.Expense.Any(e => e.ExpenseId == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
