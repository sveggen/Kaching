using AutoMapper;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IEEventRepository _eEventRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ExpenseService(
            IExpenseRepository expenseRepository,
            IEEventRepository eEventRepository,
            ITransferRepository transferRepository,
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _eEventRepository = eEventRepository;
            _transferRepository = transferRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task CreateExpense(ExpenseCreateRecurringVM expenseCreateRecurringVM)
        {
            List<EEvent> expenseEvents = new List<EEvent>();

            var paymentDate = expenseCreateRecurringVM.StartDate;
            var endDate = expenseCreateRecurringVM.EndDate;

            while (paymentDate <= endDate)
            {
                paymentDate = NextPaymentDate(paymentDate, expenseCreateRecurringVM.Frequency);

                if (paymentDate > endDate)
                {
                    break;
                }

                expenseEvents.Add(new EEvent
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
            List<EEvent> expenseEvents = new List<EEvent>();
            expenseEvents.Add(new EEvent
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
            var expenseEvent = await _eEventRepository.GetEEventById(expenseEventId);
            var expense = await _expenseRepository.GetExpenseById(expenseEvent.ExpenseId);

            foreach (var item in expense.ExpenseEvents)
            {
                _eEventRepository.DeleteEEvent(item);
            }

            _expenseRepository.DeleteExpense(expense);
            await _expenseRepository.SaveAsync();
        }

        public async Task DeleteExpense(int expenseEventId)
        {
            var expenseEvent = await _eEventRepository.GetEEventById(expenseEventId);

            _eEventRepository.DeleteEEvent(expenseEvent);
            await _eEventRepository.SaveAsync();
        }

        public async Task<EEventVM> GetExpense(int expenseEventId)
        {
            var expense = await _eEventRepository.GetEEventById(expenseEventId);
            return _mapper.Map<EEventVM>(expense);
        }

        public async Task<ExpenseIndexVM> GetExpensesByMonth(int monthNumber)
        {
            var expensesByMonth = await _eEventRepository.GetEEvents(monthNumber);
            var persons = _personRepository.GetAllPersons();
            var sum = _eEventRepository.GetExpenseEventSum(monthNumber);

            // Total amount / nr. of persons
            var share = sum / persons.Count;
            var personViewModelList = _mapper.Map<List<PersonVM>>(persons);

            int index = 0;
            foreach (var person in personViewModelList)
            {
                var sumPersonExpenses = _eEventRepository.GetSumOfPersonExpenseEvents(person.PersonId, monthNumber);
                
                personViewModelList[index].SumOfExpenses = sumPersonExpenses;
                personViewModelList[index].PaymentsSent = _transferRepository.GetSumOfPersonSentTransfers(monthNumber, person.PersonId);
                personViewModelList[index].PaymentsReceived = _transferRepository.GetSumOfPersonReceivedTransfers(monthNumber, person.PersonId);
                // For each person: amount paid - share = owes/owed
                personViewModelList[index].OwesOwed = personViewModelList[index].SumOfExpenses - share + personViewModelList[index].PaymentsSent - personViewModelList[index].PaymentsReceived;
                index++;
            }

            var expenseIndexModel = new ExpenseIndexVM
            {
                Sum = sum,
                MonthNumber = monthNumber,
                Year = 2022,
                Expenses = _mapper.Map<List<EEventVM>>(expensesByMonth),
                Persons = personViewModelList,
                CurrentDate = DateOnly.FromDateTime(DateTime.Now).ToString("dddd, dd. MMMM")
            };
            return expenseIndexModel;
        }

        public async Task UpdateExpense(ExpenseEditVM expenseEditVM)
        {
            var expense = _mapper.Map<EEvent>(expenseEditVM);
            _eEventRepository.UpdateExpenseEvent(expense);
            await _expenseRepository.SaveAsync();
        }

        public async Task UpdateExpense(ExpenseEditRecurringVM expenseEditRecurringVM)
        {
            var expense = _mapper.Map<EEvent>(expenseEditRecurringVM);
            _eEventRepository.UpdateExpenseEvent(expense);
            await _expenseRepository.SaveAsync();
        }

        public List<PersonVM> GetPersons()
        {
            var persons = _personRepository.GetAllPersons();
            return _mapper.Map<List<PersonVM>>(persons);
        }

        public List<PersonVM> GetPersonsWithoutYourself(string username)
        {
            var persons = _personRepository.GetAllPersonsWithoutYourself(username);
            return _mapper.Map<List<PersonVM>>(persons);
        }

        public PersonVM GetPersonByUsername(string userName)
        {
            var person = _personRepository.GetPersonByUserName(userName);
            return _mapper.Map<PersonVM>(person);
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

        public Task UpdateRecurringExpenses(EEventVM expenseVM)
        {
            throw new NotImplementedException();
        }
    }
}