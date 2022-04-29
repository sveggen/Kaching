using Kaching.ViewModels;

namespace Kaching.Services
{
    public interface IExpenseService
    {
        public Task<ExpenseEventViewModel> GetExpense(int expenseId);

        public Task<ExpenseIndexViewModel> GetExpensesByMonth(int monthNumber);

        public Task CreateExpense(ExpenseEventCreateViewModel expenseVM);

        public Task DeleteExpense(int expenseId);

        public Task UpdateExpense(ExpenseEventViewModel expenseVM);

        public PersonViewModel GetPerson(int personId);

        public PersonViewModel GetPersonByUsername(string userName);

        public List<PersonViewModel> GetPersons();
    }
}
