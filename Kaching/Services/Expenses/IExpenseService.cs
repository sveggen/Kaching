using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IExpenseService
    {
        public Task<ExpenseVm> GetExpense(int expenseEventId);

        public Task<List<ExpenseVm>> GetExpensesByMonth(int monthNumber, int year, int groupId);
        
        public Task CreateExpense(ExpenseCreateVm expenseCreateVm);

        public Task DeleteExpense(int expenseEventId);

        public Task PayExpense(int expenseId, int buyerId);

        public Task DeleteRecurringExpense(int expenseEventId);

        public Task UpdateExpense(ExpenseEditVm expenseEditVm);

        public Task UpdateRecurringExpenses(ExpenseEditVm expenseEditVm);

        public List<CategoryVm> GetCategories();
        
    }
}
