using AutoMapper;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseEventRepository _expenseEventRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ExpenseService(
            IExpenseRepository expenseRepository,
            IExpenseEventRepository expenseEventRepository,
            IPaymentRepository paymentRepository,
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _expenseEventRepository = expenseEventRepository;
            _paymentRepository = paymentRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task CreateExpense(ExpenseCreateRecurringVM expenseCreateRecurringVM)
        {
            List<ExpenseEvent> expenseEvents = new List<ExpenseEvent>();

            var paymentDate = expenseCreateRecurringVM.StartDate;
            var endDate = expenseCreateRecurringVM.EndDate;

            while (paymentDate <= endDate)
            {
                paymentDate = NextPaymentDate(paymentDate, expenseCreateRecurringVM.Frequency);

                if (paymentDate > endDate)
                {
                    break;
                }

                expenseEvents.Add(new ExpenseEvent
                {
                    BuyerId = expenseCreateRecurringVM.BuyerId,
                    PaymentDate = paymentDate,
                    PaymentStatus = expenseCreateRecurringVM.PaymentStatus,
                    ExpenseId = expenseCreateRecurringVM.ExpenseId
                });
            }
            var expense = _mapper.Map<Expense>(expenseCreateRecurringVM);
            expense.ExpenseEvents = expenseEvents;
            _expenseRepository.InsertExpense(expense);
            await _expenseRepository.SaveAsync();
        }

        public async Task CreateExpense(ExpenseCreateVM expenseCreateVM)
        {
            List<ExpenseEvent> expenseEvents = new List<ExpenseEvent>();
            expenseEvents.Add(new ExpenseEvent
                {
                    BuyerId = expenseCreateVM.BuyerId,
                    PaymentDate = expenseCreateVM.PaymentDate,
                    PaymentStatus = expenseCreateVM.PaymentStatus,
                    ExpenseId = expenseCreateVM.ExpenseId
                });

            var expense = _mapper.Map<Expense>(expenseCreateVM);
            expense.ExpenseEvents = expenseEvents;
            _expenseRepository.InsertExpense(expense);
            await _expenseRepository.SaveAsync();
        }

        public async Task DeleteRecurringExpense(int expenseEventId)
        {
            var expenseEvent = await _expenseEventRepository.GetExpenseEventById(expenseEventId);
            var expense = await _expenseRepository.GetExpenseById(expenseEvent.ExpenseId);

            foreach (var item in expense.ExpenseEvents)
            {
                _expenseEventRepository.DeleteExpenseEvent(expenseEvent);
            }

            _expenseRepository.DeleteExpense(expense);
            await _expenseRepository.SaveAsync();
        }

        public async Task DeleteExpense(int expenseEventId)
        {
            var expenseEvent = await _expenseEventRepository.GetExpenseEventById(expenseEventId);

            _expenseEventRepository.DeleteExpenseEvent(expenseEvent);
            await _expenseEventRepository.SaveAsync();
        }

        public async Task<ExpenseEventViewModel> GetExpense(int expenseEventId)
        {
            var expense = await _expenseEventRepository.GetExpenseEventById(expenseEventId);
            return _mapper.Map<ExpenseEventViewModel>(expense);
        }

        public async Task<ExpenseIndexViewModel> GetExpensesByMonth(int monthNumber)
        {
            var expensesByMonth = await _expenseEventRepository.GetExpenseEvents(monthNumber);
            var persons = _personRepository.GetAllPersons();
            var sum = _expenseEventRepository.GetExpenseEventSum(monthNumber);

            // Total amount / nr. of persons
            var share = sum / persons.Count;
            var personViewModelList = _mapper.Map<List<PersonViewModel>>(persons);

            int index = 0;
            foreach (var person in personViewModelList)
            {
                var sumPersonExpenses = _expenseEventRepository.GetSumOfPersonExpenseEvents(person.PersonId, monthNumber);
                
                personViewModelList[index].SumOfExpenses = sumPersonExpenses;
                personViewModelList[index].PaymentsSent = _paymentRepository.GetSumOfPersonSentPayments(monthNumber, person.PersonId);
                personViewModelList[index].PaymentsReceived = _paymentRepository.GetSumOfPersonReceivedPayments(monthNumber, person.PersonId);
                // For each person: amount paid - share = owes/owed
                personViewModelList[index].OwesOwed = personViewModelList[index].SumOfExpenses - share + personViewModelList[index].PaymentsSent - personViewModelList[index].PaymentsReceived;
                index++;
            }

            var expenseIndexModel = new ExpenseIndexViewModel
            {
                Sum = sum,
                MonthNumber = monthNumber,
                Year = 2022,
                Expenses = _mapper.Map<List<ExpenseEventViewModel>>(expensesByMonth),
                Persons = personViewModelList,
                CurrentDate = DateOnly.FromDateTime(DateTime.Now).ToString("dddd, dd. MMMM")
            };
            return expenseIndexModel;
        }

        public async Task UpdateExpense(ExpenseEventViewModel expenseEventViewModel)
        {
            var expense = _mapper.Map<ExpenseEvent>(expenseEventViewModel);
            _expenseEventRepository.UpdateExpenseEvent(expense);
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

        public List<PersonViewModel> GetPersonsWithoutYourself(string username)
        {
            var persons = _personRepository.GetAllPersonsWithoutYourself(username);
            return _mapper.Map<List<PersonViewModel>>(persons);
        }

        public PersonViewModel GetPersonByUsername(string userName)
        {
            var person = _personRepository.GetPersonByUserName(userName);
            return _mapper.Map<PersonViewModel>(person);
        }

        private DateTime NextPaymentDate(DateTime currPaymentDate, Frequency frequency)
        {
            var dateTime = currPaymentDate;

            switch (frequency)
            {
                case Frequency.Weekly:
                    return dateTime.AddDays(7);
                case Frequency.Monthly:
                    return dateTime.AddMonths(1);
                case Frequency.Yearly:
                    return dateTime.AddYears(1);
            }

            return dateTime;
        }

        public Task UpdateRecurringExpenses(ExpenseEventViewModel expenseVM)
        {
            throw new NotImplementedException();
        }
    }
}
