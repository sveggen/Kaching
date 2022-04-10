using Kaching.Data;
using Kaching.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kaching.Repositories
{
    public class ExpenseRepository : IExpenseRepository, IDisposable
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
                .Include(e => e.Person)
                .FirstOrDefaultAsync(m => m.ExpenseId == expenseId);
        }

        public async Task<List<Expense>> GetExpenses(int monthNumber)
        {
            return await _context.Expense
                .Include(e => e.Person)
                .Where(p => p.Created.Month == monthNumber)
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

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
