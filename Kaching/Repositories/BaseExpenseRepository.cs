using Kaching.Data;
using Kaching.Enums;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Repositories
{
    public class BaseExpenseRepository : IBaseExpenseRepository, IDisposable
    {

        private readonly DataContext _context;

        public BaseExpenseRepository(DataContext context)
        {
            _context = context;
        }

        public void InsertExpense(BaseExpense baseExpense)
        {
            _context.Add(baseExpense);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateExpense(BaseExpense baseExpense)
        {
            _context.Update(baseExpense);
        }

        public bool GetExpenseExistence(int id)
        {
            return _context.BaseExpense.Any(e => e.BaseExpenseId == id);
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

        public async Task<List<BaseExpense>> GetExpenses(int monthNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BaseExpense>> GetPersonExpenses(int personId, int monthNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseExpense> GetExpenseById(int expenseId)
        {
            return await _context.BaseExpense
                .Include(e => e.Expenses)
                .FirstOrDefaultAsync(e => e.BaseExpenseId == expenseId);
        }

        public void DeleteExpense(BaseExpense baseExpense)
        {
            _context
                .BaseExpense.Remove(baseExpense);
        }
    }
}


