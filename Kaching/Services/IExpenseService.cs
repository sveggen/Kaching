using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IExpenseService
    {
        public Task<ExpenseVm> GetExpense(int expenseEventId);

        public Task<ExpensesByMonthVm> GetExpensesByMonth(int monthNumber, string year);

        public Task CreateExpense(ExpenseCreateVm expenseCreateVm);

      //  public Task CreateExpense(ExpenseCreateRecurringVM expenseCreateRecurringVM);

        public Task DeleteExpense(int expenseEventId);

        public Task DeleteRecurringExpense(int expenseEventId);

        public Task UpdateExpense(ExpenseEditVm expenseEditVm);

     //   public Task UpdateExpense(ExpenseEditRecurringVm expenseEditRecurringVM);

        public Task UpdateRecurringExpenses(ExpenseVm expenseVm);

    
    }
}
