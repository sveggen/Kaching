using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IExpenseService
    {
        public Task<ExpenseViewModel> GetExpense(int expenseId);

        public Task<ExpenseIndexViewModel> GetExpensesByMonth(int monthNumber);

        public Task CreateExpense(ExpenseViewModel expenseVM);

        public Task DeleteExpense(int expenseId);

        public Task UpdateExpense(ExpenseViewModel expenseVM);

        public PersonViewModel GetPerson(int personId);

        public PersonViewModel GetPersonByUsername(string userName);

        public List<PersonViewModel> GetPersons();
    }
}
