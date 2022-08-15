using Kaching.Data;
using Kaching.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Repositories
{
    public class ExpenseRepository : IExpenseRepository, IDisposable
    {

        private readonly DataContext _context;

        public ExpenseRepository(DataContext context)
        {
            _context = context;
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

        public async Task<List<Expense>> GetExpenses(int monthNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Expense>> GetPersonExpenses(int personId, int monthNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<Expense> GetExpenseById(int expenseId)
        {
            return await _context.Expense
                .Include(e => e.ExpenseEvents)
                .FirstOrDefaultAsync(e => e.ExpenseId == expenseId);
        }

        public void DeleteExpense(Expense expense)
        {
            _context
                .Expense.Remove(expense);
        }
    }
}


