using Kaching.Data;
using Kaching.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Repositories
{
    public class PaymentRepository : IPaymentRepository, IDisposable
    {
        private readonly DataContext _context;

        public PaymentRepository(DataContext context)
        {
            _context = context;
        }

        public void DeletePayment(Payment payment)
        {
            throw new NotImplementedException();
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
        public Task<Payment> GetPaymentById(int paymentId)
        {
            throw new NotImplementedException();
        }

        public bool GetPaymentExistence(int paymentId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Payment>> GetPayments()
        {
            return await _context.Payment
                .Include(e => e.Sender)
                .Include(f=> f.Receiver)
                .OrderByDescending(e=>e.PaymentPeriod)
                .ToListAsync(); 
        }

        public Task<List<Payment>> GetPersonPayments(int personId, int monthNumber)
        {
            throw new NotImplementedException();
        }

        public void InsertPayment(Payment payment)
        {
            _context.Add(payment);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public decimal GetSumOfPersonSentPayments(int monthNumber, int personId)
        {
            return _context.Payment
                .Include(e => e.Sender)
                .Where(p => p.Sender.PersonId == personId)
                .Where(p => p.PaymentPeriod.Month == monthNumber)
                .Sum(i => i.Amount);
        }
        public decimal GetSumOfPersonReceivedPayments(int monthNumber, int personId)
        {
            return _context.Payment
                .Include(e => e.Receiver)
                .Where(p => p.Receiver.PersonId == personId)
                .Where(p => p.PaymentPeriod.Month == monthNumber)
                .Sum(i => i.Amount);
        }
    }
}
