using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IExpenseEventRepository
    {
        public void DeleteExpenseEvents(ExpenseEvent expenseEvent);

        public Task<ExpenseEvent> GetExpenseEventsById(int expenseEventId);

        public Task<List<ExpenseEvent>> GetExpenseEvents(int monthNumber);

        public decimal GetExpenseEventsSum(int monthNumber);

        public Task<List<ExpenseEvent>> GetPersonExpenseEvents(int personId, int monthNumber);

        public decimal GetSumOfPersonExpenseEvents(int personId, int monthNumber);

        public void InsertExpenseEvent(ExpenseEvent expenseEvent);

        public void Save();

        public void UpdateExpenseEvent(ExpenseEvent expenseEvent);

        public bool GetExpenseExistence(int id);



    }
}
