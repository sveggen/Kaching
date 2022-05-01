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
        public async Task CreatePayment(PaymentCreateViewModel paymentVM)
        {
            var payment = _mapper.Map<Payment>(paymentVM);
            _paymentRepository.InsertPayment(payment);
            await _paymentRepository.SaveAsync();
        }

        public async Task DeletePayment(int paymentId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentViewModel> GetPayment(int paymentId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentIndexViewModel> GetPaymentsByMonth(int monthNumber)
        {
          //  var sum = _paymentRepository.GetPaymentsSum(monthNumber);
            var payments = await _paymentRepository.GetPayments(monthNumber);

           // var personViewModelList = _mapper.Map<List<PersonViewModel>>(persons);

           return null;
        }
    }
}
