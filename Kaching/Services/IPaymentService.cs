using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IPaymentService
    {
        public Task<PaymentViewModel> GetPayment(int paymentId);

        public Task<PaymentIndexViewModel> GetPaymentsByMonth(int monthNumber);

        public Task CreatePayment(PaymentCreateViewModel paymentVM);

        public Task DeletePayment(int paymentId);

    }

}
