using Kaching.Models;

namespace Kaching.Repositories
{
    public interface ITransferRepository
    {

        public Task<List<Transfer>> GetTransfers(int groupId);

        public Task<List<Transfer>> GetPersonTransfers(int personId, int monthNumber);

        public decimal GetSumOfPersonSentTransfers(int monthNumber, int personId);

        public decimal GetSumOfPersonReceivedTransfers(int monthNumber, int personId);

        public Task<Transfer> GetTransferById(int transferId);

        public bool GetTransferExistence(int transferId);

        public void InsertTransfer(Transfer transfer);

        public void DeleteTransfer(Transfer transfer);

        public void Save();

        public Task SaveAsync();

    }
}
