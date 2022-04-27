using AutoMapper;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;


        public ExpenseService(
            IExpenseRepository expenseRepository,
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }
        public async Task CreateExpense(ExpenseViewModel expenseVM)
        {
            var expense = _mapper.Map<Expense>(expenseVM);

            _expenseRepository.InsertExpense(expense);
            await _expenseRepository.SaveAsync();
        }

        public async Task DeleteExpense(int expenseId)
        {
            var expense = await _expenseRepository.GetExpenseById(expenseId);
            _expenseRepository.DeleteExpense(expense);
            await _expenseRepository.SaveAsync();
        }

        public async Task<ExpenseViewModel> GetExpense(int expenseId)
        {
            var expense = await _expenseRepository.GetExpenseById(expenseId);
            return _mapper.Map<ExpenseViewModel>(expense);

        }

        public async Task<ExpenseIndexViewModel> GetExpensesByMonth(int monthNumber)
        {
            var expensesByMonth = await _expenseRepository.GetExpenses(monthNumber);
            var persons = _personRepository.GetAllPersons();
            var sum = _expenseRepository.GetExpenseSum(monthNumber);

            // Total amount / nr. of persons
            var share = sum / persons.Count;

            var personViewModelList = _mapper.Map<List<PersonViewModel>>(persons);


            int index = 0;
            foreach (var person in personViewModelList)
            {
                var sumPersonExpenses = _expenseRepository.GetSumOfPersonExpenses(person.PersonId, monthNumber);
                personViewModelList[index].SumOfExpenses = sumPersonExpenses;
                // For each person: amount paid - share = owes/owed
                personViewModelList[index].OwesOwed = personViewModelList[index].SumOfExpenses - share;
                index++;
            }

            var expenseIndexModel = new ExpenseIndexViewModel
            {
                Sum = sum,
                MonthNumber = monthNumber,
                Year = 2022,
                Expenses = _mapper.Map<List<ExpenseViewModel>>(expensesByMonth),
                Persons = personViewModelList,
                CurrentDate = DateOnly.FromDateTime(DateTime.Now).ToString("dddd, dd. MMMM")
            };
            return expenseIndexModel;
        }

        public async Task UpdateExpense(ExpenseViewModel expenseVM)
        {
            var expense = _mapper.Map<Expense>(expenseVM);
            _expenseRepository.UpdateExpense(expense);
            await _expenseRepository.SaveAsync();
        }

        public PersonViewModel GetPerson(int personId)
        {
            var person = _personRepository.GetPersonByPersonId(personId);
            return _mapper.Map<PersonViewModel>(person);
        }

        public List<PersonViewModel> GetPersons()
        {
            var persons = _personRepository.GetAllPersons();
            return _mapper.Map<List<PersonViewModel>>(persons);
        }

        public PersonViewModel GetPersonByUsername(string userName)
        {
            var person = _personRepository.GetPersonByUserName(userName);
            return _mapper.Map<PersonViewModel>(person);
        }
    }
}
