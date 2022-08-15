using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IPaymentService
    {
        public Task<PaymentVM> GetPayment(int paymentId);

        public Task<List<PaymentVM>> GetPayments();

        public Task CreatePayment(PaymentCreateVM paymentVM);

        public Task DeletePayment(int paymentId);

    }

}
