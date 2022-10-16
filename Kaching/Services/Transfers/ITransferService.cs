using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface ITransferService
    {
        public Task<TransferVm> GetTransfer(int transferId);

        public Task<List<TransferVm>> GetTransfers(int groupId);
        
        public Task<List<TransferVm>> GetTransfersByMonth(int groupId, string month);

        public Task CreateTransfer(TransferCreateVM transferVM);

        public Task DeleteTransfer(int transferId);

        public Task<List<CurrencyVm>> GetAllCurrencies();
    }

}
