using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IExpenseService
    {
        public Task<ExpenseVm> GetExpense(int expenseEventId);

        public Task<List<ExpenseVm>> GetExpensesByMonth(int monthNumber, int year, int groupId);

        public Task<List<ExpensePersonalVm>> GetPersonalExpensesByMonth(int monthNumber, int year, int groupId);

        public Task CreateExpense(ExpenseCreateVm expenseCreateVm);

      //  public Task CreateExpense(ExpenseCreateRecurringVM expenseCreateRecurringVM);

        public Task DeleteExpense(int expenseEventId);

        public Task DeleteRecurringExpense(int expenseEventId);

        public Task UpdateExpense(ExpenseEditVm expenseEditVm);

     //   public Task UpdateExpense(ExpenseEditRecurringVm expenseEditRecurringVM);

        public Task UpdateRecurringExpenses(ExpenseVm expenseVm);

        public List<CategoryVm> GetCategories();
        
    }
}
