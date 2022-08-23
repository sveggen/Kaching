﻿using AutoMapper;
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

        public Task<ExpensesByMonthVm> GetPersonalExpensesByMonth(int monthNumber, string year)
        {
            throw new NotImplementedException();
        }

        public async Task CreateExpense(ExpenseCreateVm expenseCreateVm)
        {
            if (expenseCreateVm.Frequency == Frequency.Once)
            {
                await CreateSingleExpense(expenseCreateVm);
            }
            else
            {
                await CreateRecurringExpense(expenseCreateVm);
            }
        }

        public async Task DeleteRecurringExpense(int expenseEventId)
        {
            var expenseEvent = await _expenseRepository.GetExpenseById(expenseEventId);
            var expense = await _baseExpenseRepository.GetBaseExpenseById(expenseEvent.ExpenseId);

            foreach (var item in expense.Expenses)
            {
                _expenseRepository.DeleteExpense(item);
            }

            _baseExpenseRepository.DeleteBaseExpense(expense);
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
        
        

        public async Task<ExpensesByMonthVm> GetExpensesByMonth(int monthNumber, string year)
        {
            var expensesByMonth = await _expenseRepository.GetExpenses(monthNumber, year);
            var sum = _expenseRepository.GetSumExpensesByMonth(monthNumber, year);
            
            /*var persons = _personRepository.GetAllPersons();

            // Total amount / nr. of persons
            var share = sum / persons.Count;
            var personVmList = _mapper.Map<List<PersonLightVm>>(persons);

            int index = 0;
            foreach (var person in personVmList)
            {
                var sumPersonExpenses = _expenseRepository.GetSumOfPersonExpensesByMonth(person.PersonId, monthNumber);
                
                personVmList[index].SumOfExpenses = sumPersonExpenses;
                personVmList[index].PaymentsSent = _transferRepository.GetSumOfPersonSentTransfers(monthNumber, person.PersonId);
                personVmList[index].PaymentsReceived = _transferRepository.GetSumOfPersonReceivedTransfers(monthNumber, person.PersonId);
                // For each person: amount paid - share = owes/owed
                personVmList[index].OwesOwed = personVmList[index].SumOfExpenses - share + personVmList[index].PaymentsSent - personVmList[index].PaymentsReceived;
                index++;
            }*/

            var expenseIndexModel = new ExpensesByMonthVm
            {
                Sum = sum,
                MonthNumber = monthNumber,
                Year = 2022,
                Expenses = _mapper.Map<List<ExpenseVm>>(expensesByMonth),
                //Persons = personVmList,
                CurrentDate = DateOnly.FromDateTime(DateTime.Now).ToString("dddd, dd. MMMM")
            };
            return expenseIndexModel;
        }

        public async Task UpdateExpense(ExpenseEditVm expenseEditVm)
        {
            var expense = _mapper.Map<Expense>(expenseEditVm);
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

        public Task UpdateRecurringExpenses(ExpenseVm expenseVm)
        {
            throw new NotImplementedException();
        }

        private async Task CreateSingleExpense(ExpenseCreateVm expenseCreateVm)
        {
            List<Expense> expenseEvents = new List<Expense>();
            expenseEvents.Add(new Expense
            {
                BuyerId = expenseCreateVm.BuyerId,
                PaymentDate = expenseCreateVm.PaymentDate,
                Price = expenseCreateVm.Price,
                PaymentType = expenseCreateVm.PaymentType,
                ExpenseId = expenseCreateVm.ExpenseId
            });

            var expense = _mapper.Map<BaseExpense>(expenseCreateVm);
            expense.Expenses = expenseEvents;
            _baseExpenseRepository.InsertBaseExpense(expense);
            await _baseExpenseRepository.SaveAsync();
        }
        private async Task CreateRecurringExpense(ExpenseCreateVm expenseCreateVm)
        {
            List<Expense> expenseEvents = new List<Expense>();

            var paymentDate = expenseCreateVm.PaymentDate;
            const int recurringExpenseMaxCap = 3;
            var endDate = paymentDate.AddYears(recurringExpenseMaxCap);

            while (paymentDate <= endDate)
            {
                paymentDate = NextPaymentDate(paymentDate, expenseCreateVm.Frequency);

                if (paymentDate > endDate)
                {
                    break;
                }

                expenseEvents.Add(new Expense
                {
                    BuyerId = expenseCreateVm.BuyerId,
                    PaymentDate = paymentDate,
                    Price = expenseCreateVm.Price,
                    PaymentType = expenseCreateVm.PaymentType,
                    ExpenseId = expenseCreateVm.ExpenseId
                });
            }
            var expense = _mapper.Map<BaseExpense>(expenseCreateVm);
            expense.Expenses = expenseEvents;
            _baseExpenseRepository.InsertBaseExpense(expense);
            await _baseExpenseRepository.SaveAsync();
        }
    }
}