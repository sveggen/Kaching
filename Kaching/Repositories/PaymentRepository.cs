using Kaching.Data;
using Kaching.Models;

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

        public Task<List<Payment>> GetPayments(int monthNumber)
        {
            throw new NotImplementedException();
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
    }
}
