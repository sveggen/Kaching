using Kaching.Data;
using Kaching.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kaching.Repositories
{
    public class ExpenseEventRepository : IExpenseEventRepository, IDisposable
    {
        private readonly DataContext _context;

        public ExpenseEventRepository(DataContext context)
        {
            _context = context;
        }   

        public void DeleteExpenseEvent(ExpenseEvent expenseEvent)
        {
             _context
                .ExpenseEvent.Remove(expenseEvent);
        }

        public async Task<ExpenseEvent> GetExpenseEventById(int expenseEventId)
        {
            return await _context.ExpenseEvent
                .Include(e => e.Expense)
                .Include(b => b.Buyer)
                .FirstOrDefaultAsync(m => m.ExpenseEventId == expenseEventId);
        }

        public async Task<List<ExpenseEvent>> GetExpenseEvents(int monthNumber)
        {
            return await _context.ExpenseEvent
                .Include(e => e.Buyer)
                .Include(e => e.Expense)
                .Where(p => p.PaymentDate.Month == monthNumber)
                .ToListAsync();
        }

        public decimal GetExpenseEventSum(int monthNumber)
        {
            return  _context.ExpenseEvent
                .Include(e => e.Buyer)
                .Include(e => e.Expense)
                .Where(p => p.PaymentDate.Month == monthNumber)
                .Sum(i => i.Expense.Price);
        }


        public async Task<List<ExpenseEvent>> GetPersonExpenseEvents(int personId, int monthNumber)
        {
            return await _context.ExpenseEvent
                .Include(e => e.Expense)
                .Where(p => p.PaymentDate.Month == monthNumber)
                .Where(e => e.BuyerId == personId)
                .ToListAsync();
        }

        public decimal GetSumOfPersonExpenseEvents(int personId, int monthNumber)
        {
            return _context.ExpenseEvent
                .Include(v => v.Expense)
                .Include(b => b.Buyer)
                .Where(p => p.PaymentDate.Month == monthNumber)
                .Where(e => e.BuyerId == personId)
                .Sum(e => e.Expense.Price);
        }

        public void InsertExpenseEvent(ExpenseEvent expenseEvent)
        {
            _context.Add(expenseEvent);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateExpenseEvent(ExpenseEvent expenseEvent)
        {
            _context.Update(expenseEvent);
        }

        public bool GetExpenseExistence(int id)
        {
            return _context.ExpenseEvent.Any(e => e.ExpenseEventId == id);
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
