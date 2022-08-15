using Kaching.Data;
using Kaching.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kaching.Repositories
{
    public class EEventRepository : IEEventRepository, IDisposable
    {
        private readonly DataContext _context;

        public EEventRepository(DataContext context)
        {
            _context = context;
        }   

        public void DeleteEEvent(EEvent eEvent)
        {
             _context
                .ExpenseEvent.Remove(eEvent);
        }

        public async Task<EEvent> GetEEventById(int eEventId)
        {
            return await _context.ExpenseEvent
                .Include(e => e.Expense)
                .Include(b => b.Buyer)
                .FirstOrDefaultAsync(m => m.EEventId == eEventId);
        }

        public async Task<List<EEvent>> GetEEvents(int monthNumber)
        {
            return await _context.ExpenseEvent
                .Include(e => e.Buyer)
                .Include(e => e.Expense)
                .Where(p => p.PaymentDate.Month == monthNumber)
                .OrderByDescending(e => e.PaymentDate)
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


        public async Task<List<EEvent>> GetPersonExpenseEvents(int personId, int monthNumber)
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

        public void InsertExpenseEvent(EEvent eEvent)
        {
            _context.Add(eEvent);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateExpenseEvent(EEvent eEvent)
        {
            _context.Update(eEvent);
        }

        public bool GetExpenseExistence(int id)
        {
            return _context.ExpenseEvent.Any(e => e.EEventId == id);
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
