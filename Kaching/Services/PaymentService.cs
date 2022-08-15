using AutoMapper;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }
        public async Task CreatePayment(PaymentCreateVM paymentVM)
        {
            var payment = _mapper.Map<Payment>(paymentVM);
            _paymentRepository.InsertPayment(payment);
            await _paymentRepository.SaveAsync();
        }

        public async Task DeletePayment(int paymentId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentVM> GetPayment(int paymentId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PaymentVM>> GetPayments()
        {
            var payments = await _paymentRepository.GetPayments();

            return _mapper.Map<List<PaymentVM>>(payments);
        }
    }
}
