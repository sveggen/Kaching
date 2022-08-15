using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IExpenseService
    {
        public Task<EEventVM> GetExpense(int expenseEventId);

        public Task<ExpenseIndexVM> GetExpensesByMonth(int monthNumber);

        public Task CreateExpense(ExpenseCreateVM expenseCreateVM);

        public Task CreateExpense(ExpenseCreateRecurringVM expenseCreateRecurringVM);

        public Task DeleteExpense(int expenseEventId);

        public Task DeleteRecurringExpense(int expenseEventId);

        public List<PersonVM> GetPersonsWithoutYourself(string username);

        public Task UpdateExpense(ExpenseEditVM expenseEditVM);

        public Task UpdateExpense(ExpenseEditRecurringVM expenseEditRecurringVM);

        public Task UpdateRecurringExpenses(EEventVM expenseVM);

        public PersonVM GetPersonByUsername(string userName);

        public List<PersonVM> GetPersons();
    }
}
