using AutoMapper;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IBaseExpenseRepository _baseExpenseRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ExpenseService(
            IBaseExpenseRepository baseExpenseRepository,
            IExpenseRepository expenseRepository,
            ITransferRepository transferRepository,
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _baseExpenseRepository = baseExpenseRepository;
            _expenseRepository = expenseRepository;
            _transferRepository = transferRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task CreateExpense(ExpenseCreateRecurringVM expenseCreateRecurringVM)
        {
            List<Expense> expenseEvents = new List<Expense>();

            var paymentDate = expenseCreateRecurringVM.StartDate;
            var endDate = paymentDate.AddYears(3);

            while (paymentDate <= endDate)
            {
                paymentDate = NextPaymentDate(paymentDate, expenseCreateRecurringVM.Frequency);

                if (paymentDate > endDate)
                {
                    break;
                }

                expenseEvents.Add(new Expense
                {
                    BuyerId = expenseCreateRecurringVM.BuyerId,
                    PaymentDate = paymentDate,
                    Price = expenseCreateRecurringVM.Price,
                    PaymentType = expenseCreateRecurringVM.PaymentType,
                    ExpenseId = expenseCreateRecurringVM.ExpenseId
                });
            }
            var expense = _mapper.Map<BaseExpense>(expenseCreateRecurringVM);
            expense.Expenses = expenseEvents;
            _baseExpenseRepository.InsertExpense(expense);
            await _baseExpenseRepository.SaveAsync();
        }

        public async Task CreateExpense(ExpenseCreateVM expenseCreateVM)
        {
            List<Expense> expenseEvents = new List<Expense>();
            expenseEvents.Add(new Expense
                {
                    BuyerId = expenseCreateVM.BuyerId,
                    PaymentDate = expenseCreateVM.PaymentDate,
                    Price = expenseCreateVM.Price,
                    PaymentType = expenseCreateVM.PaymentType,
                    ExpenseId = expenseCreateVM.ExpenseId
                });

            var expense = _mapper.Map<BaseExpense>(expenseCreateVM);
            expense.Expenses = expenseEvents;
            _baseExpenseRepository.InsertExpense(expense);
            await _baseExpenseRepository.SaveAsync();
        }

        public async Task DeleteRecurringExpense(int expenseEventId)
        {
            var expenseEvent = await _expenseRepository.GetExpenseById(expenseEventId);
            var expense = await _baseExpenseRepository.GetExpenseById(expenseEvent.ExpenseId);

            foreach (var item in expense.Expenses)
            {
                _expenseRepository.DeleteExpense(item);
            }

            _baseExpenseRepository.DeleteExpense(expense);
            await _baseExpenseRepository.SaveAsync();
        }

        public async Task DeleteExpense(int expenseEventId)
        {
            var expenseEvent = await _expenseRepository.GetExpenseById(expenseEventId);

            _expenseRepository.DeleteExpense(expenseEvent);
            await _expenseRepository.SaveAsync();
        }

        public async Task<ExpenseVm> GetExpense(int expenseEventId)
        {
            var expense = await _expenseRepository.GetExpenseById(expenseEventId);
            return _mapper.Map<ExpenseVm>(expense);
        }

        public async Task<ExpensesByMonthVm> GetExpensesByMonth(int monthNumber)
        {
            var expensesByMonth = await _expenseRepository.GetExpenses(monthNumber);
            var persons = _personRepository.GetAllPersons();
            var sum = _expenseRepository.GetSumExpensesByMonth(monthNumber);

            // Total amount / nr. of persons
            var share = sum / persons.Count;
            var personViewModelList = _mapper.Map<List<PersonLightVm>>(persons);

            int index = 0;
            foreach (var person in personViewModelList)
            {
                var sumPersonExpenses = _expenseRepository.GetSumOfPersonExpensesByMonth(person.PersonId, monthNumber);
                
                personViewModelList[index].SumOfExpenses = sumPersonExpenses;
                personViewModelList[index].PaymentsSent = _transferRepository.GetSumOfPersonSentTransfers(monthNumber, person.PersonId);
                personViewModelList[index].PaymentsReceived = _transferRepository.GetSumOfPersonReceivedTransfers(monthNumber, person.PersonId);
                // For each person: amount paid - share = owes/owed
                personViewModelList[index].OwesOwed = personViewModelList[index].SumOfExpenses - share + personViewModelList[index].PaymentsSent - personViewModelList[index].PaymentsReceived;
                index++;
            }

            var expenseIndexModel = new ExpensesByMonthVm
            {
                Sum = sum,
                MonthNumber = monthNumber,
                Year = 2022,
                Expenses = _mapper.Map<List<ExpenseVm>>(expensesByMonth),
                Persons = personViewModelList,
                CurrentDate = DateOnly.FromDateTime(DateTime.Now).ToString("dddd, dd. MMMM")
            };
            return expenseIndexModel;
        }

        public async Task UpdateExpense(ExpenseEditVm expenseEditVM)
        {
            var expense = _mapper.Map<Expense>(expenseEditVM);
            _expenseRepository.UpdateExpense(expense);
            await _baseExpenseRepository.SaveAsync();
        }

        public async Task UpdateExpense(ExpenseEditRecurringVm expenseEditRecurringVM)
        {
            var expense = _mapper.Map<Expense>(expenseEditRecurringVM);
            _expenseRepository.UpdateExpense(expense);
            await _baseExpenseRepository.SaveAsync();
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

        public Task UpdateRecurringExpenses(ExpenseVm expenseVM)
        {
            throw new NotImplementedException();
        }
    }
}