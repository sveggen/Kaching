using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface ITransferService
    {
        public Task<TransferVm> GetTransfer(int transferId);

        public Task<List<TransferVm>> GetTransfers();

        public Task CreateTransfer(TransferCreateVM transferVM);

        public Task DeleteTransfer(int transferId);

    }

}
