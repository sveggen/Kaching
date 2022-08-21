using AutoMapper;
using Kaching.Enums;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;

        public TransferService(
            ITransferRepository transferRepository,
            IMapper mapper)
        {
            _transferRepository = transferRepository;
            _mapper = mapper;
        }
        public async Task CreateTransfer(TransferCreateVM transferVM)
        {
            var payment = _mapper.Map<Transfer>(transferVM);
            _transferRepository.InsertTransfer(payment);
            await _transferRepository.SaveAsync();
        }

        public async Task DeleteTransfer(int transferId)
        {
            throw new NotImplementedException();
        }

        public async Task<TransferVm> GetTransfer(int transferId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TransferVm>> GetTransfers()
        {
            var payments = await _transferRepository.GetTransfers();

            return _mapper.Map<List<TransferVm>>(payments);
        }
    }
}
