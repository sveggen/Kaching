using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IExpenseService
    {
        public Task<ExpenseVm> GetExpense(int expenseEventId);

        public Task<ExpensesByMonthVm> GetExpensesByMonth(int monthNumber);

        public Task CreateExpense(ExpenseCreateVM expenseCreateVM);

        public Task CreateExpense(ExpenseCreateRecurringVM expenseCreateRecurringVM);

        public Task DeleteExpense(int expenseEventId);

        public Task DeleteRecurringExpense(int expenseEventId);

        public Task UpdateExpense(ExpenseEditVm expenseEditVM);

        public Task UpdateExpense(ExpenseEditRecurringVm expenseEditRecurringVM);

        public Task UpdateRecurringExpenses(ExpenseVm expenseVM);

    
    }
}
