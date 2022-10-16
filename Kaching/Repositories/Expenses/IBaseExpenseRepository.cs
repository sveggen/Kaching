using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IBaseExpenseRepository : IDisposable
    {
        public Task<List<BaseExpense>> GetBaseExpenses(int monthNumber);

        public Task<List<BaseExpense>> GetPersonBaseExpenses(int personId, int monthNumber);

        public Task<BaseExpense> GetBaseExpenseById(int baseExpenseId);

        public bool GetBaseExpenseExistence(int id);

        public void InsertBaseExpense(BaseExpense baseExpense);

        public void DeleteBaseExpense(BaseExpense baseExpense);

        public void UpdateBaseExpense(BaseExpense baseExpense);

        public Task SaveAsync();
    }
}