using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IExpenseService
    {
        public Task<ExpenseEventViewModel> GetExpense(int expenseEventId);

        public Task<ExpenseIndexViewModel> GetExpensesByMonth(int monthNumber);

        public Task CreateExpense(ExpenseCreateVM expenseCreateVM);

        public Task CreateExpense(ExpenseCreateRecurringVM expenseCreateRecurringVM);

        public Task DeleteExpense(int expenseEventId);

        public Task DeleteRecurringExpense(int expenseEventId);

        public List<PersonViewModel> GetPersonsWithoutYourself(string username);

        public Task UpdateExpense(ExpenseEventViewModel expenseVM);

        public Task UpdateRecurringExpenses(ExpenseEventViewModel expenseVM);

        public PersonViewModel GetPerson(int personId);

        public PersonViewModel GetPersonByUsername(string userName);

        public List<PersonViewModel> GetPersons();
    }
}
