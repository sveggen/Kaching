using Kaching.Models;

namespace Kaching.Repositories
{
    public interface IExpenseRepository
    {
        public void DeleteExpense(Expense expense);

        public Task<Expense> GetExpenseById(int expenseId);

        public Task<List<Expense>> GetExpenses(int monthNumber, string year);

        public decimal GetSumExpensesByMonth(int monthNumber, string year);

        public Task<List<Expense>> GetPersonExpensesByMonth(int personId, int monthNumber);

        public decimal GetSumOfPersonExpensesByMonth(int personId, int monthNumber);

        public void InsertExpense(Expense expense);

        public void Save();

        public Task SaveAsync();

        public void UpdateExpense(Expense expense);

        public bool GetExpenseExistence(int id);

    }
}
