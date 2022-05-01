using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IPaymentRepository
    {

        public Task<List<Payment>> GetPayments(int monthNumber);

        public Task<List<Payment>> GetPersonPayments(int personId, int monthNumber);

        public decimal GetSumOfPersonSentPayments(int monthNumber, int personId);

        public decimal GetSumOfPersonReceivedPayments(int monthNumber, int personId);

        public Task<Payment> GetPaymentById(int paymentId);

        public bool GetPaymentExistence(int paymentId);

        public void InsertPayment(Payment payment);

        public void DeletePayment(Payment payment);

        public void Save();

        public Task SaveAsync();

    }
}
