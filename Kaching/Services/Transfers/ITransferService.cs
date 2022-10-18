using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface ITransferService
    {
        public Task<List<TransferVm>> GetTransfers(int groupId);

        public Task CreateTransfer(TransferCreateVM transferVM);

        public Task DeleteTransfer(int transferId);

        public Task<List<CurrencyVm>> GetAllCurrencies();
    }

}
