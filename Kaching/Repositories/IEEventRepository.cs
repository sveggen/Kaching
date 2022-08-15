using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IEEventRepository
    {
        public void DeleteEEvent(EEvent eEvent);

        public Task<EEvent> GetEEventById(int eEventId);

        public Task<List<EEvent>> GetEEvents(int monthNumber);

        public decimal GetExpenseEventSum(int monthNumber);

        public Task<List<EEvent>> GetPersonExpenseEvents(int personId, int monthNumber);

        public decimal GetSumOfPersonExpenseEvents(int personId, int monthNumber);

        public void InsertExpenseEvent(EEvent eEvent);

        public void Save();

        public Task SaveAsync();

        public void UpdateExpenseEvent(EEvent eEvent);

        public bool GetExpenseExistence(int id);



    }
}
