using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IBaseExpenseRepository : IDisposable
    {
        public Task<List<BaseExpense>> GetExpenses(int monthNumber);

        public Task<List<BaseExpense>> GetPersonExpenses(int personId, int monthNumber);

        public Task<BaseExpense> GetExpenseById(int expenseId);

        public bool GetExpenseExistence(int id);

        public void InsertExpense(BaseExpense baseExpense);

        public void DeleteExpense(BaseExpense baseExpense);

        public void UpdateExpense(BaseExpense baseExpense);

        public void Save();

        public Task SaveAsync();
    }
}