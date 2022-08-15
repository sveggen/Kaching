using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface ITransferService
    {
        public Task<TransferVM> GetTransfer(int transferId);

        public Task<List<TransferVM>> GetTransfers();

        public Task CreateTransfer(TransferCreateVM transferVM);

        public Task DeleteTransfer(int transferId);

    }

}
