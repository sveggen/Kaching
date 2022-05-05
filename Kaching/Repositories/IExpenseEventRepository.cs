using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IExpenseEventRepository
    {
        public void DeleteExpenseEvent(ExpenseEvent expenseEvent);

        public Task<ExpenseEvent> GetExpenseEventById(int expenseEventId);

        public Task<List<ExpenseEvent>> GetExpenseEvents(int monthNumber);

        public decimal GetExpenseEventSum(int monthNumber);

        public Task<List<ExpenseEvent>> GetPersonExpenseEvents(int personId, int monthNumber);

        public decimal GetSumOfPersonExpenseEvents(int personId, int monthNumber);

        public void InsertExpenseEvent(ExpenseEvent expenseEvent);

        public void Save();

        public Task SaveAsync();

        public void UpdateExpenseEvent(ExpenseEvent expenseEvent);

        public bool GetExpenseExistence(int id);



    }
}
