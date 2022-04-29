using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IExpenseRepository : IDisposable
    {
        public Task<List<Expense>> GetExpenses(int monthNumber);

        public Task<List<Expense>> GetPersonExpenses(int personId, int monthNumber);

        public Task<Expense> GetExpenseById(int expenseId);

        public bool GetExpenseExistence(int id);

        public void InsertExpense(Expense expense);

        public void DeleteExpense(Expense expense);

        public void UpdateExpense(Expense expense);

        public void Save();

        public Task SaveAsync();
    }
}