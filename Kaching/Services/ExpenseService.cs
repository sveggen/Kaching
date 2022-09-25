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
        private readonly IGroupRepository _groupRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ExpenseService(
            IBaseExpenseRepository baseExpenseRepository,
            IExpenseRepository expenseRepository,
            ITransferRepository transferRepository,
            IPersonRepository personRepository,
            IGroupRepository groupRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _baseExpenseRepository = baseExpenseRepository;
            _expenseRepository = expenseRepository;
            _transferRepository = transferRepository;
            _personRepository = personRepository;
            _groupRepository = groupRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ExpenseVm>> GetExpensesByMonth(int monthNumber, int year, int groupId)
        {
            var expenses = await _expenseRepository.GetGroupExpensesByMonth
                (monthNumber, year, groupId);
            return _mapper.Map<List<ExpenseVm>>(expenses);
        }

        public async Task<List<ExpensePersonalVm>> GetPersonalExpensesByMonth
            (int monthNumber, int year, int personId)
        {
            var personalGroup = _groupRepository.GetPersonalGroup(personId);
            var expenses = await _expenseRepository.GetGroupExpensesByMonth
                (monthNumber, year, personalGroup.GroupId);
            return _mapper.Map<List<ExpensePersonalVm>>(expenses);
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

        public async Task PayExpense(int expenseId, int buyerId)
        {
            var expense = await _expenseRepository.GetExpenseById(expenseId);
            
            expense.BuyerId = buyerId;
            expense.Paid = true;
            expense.PaymentDate = DateTime.Now;
            
            _expenseRepository.UpdateExpense(expense);
            await _expenseRepository.SaveAsync();
        }

        public async Task DeleteRecurringExpense(int expenseEventId)
        {
            var expense = await _expenseRepository.GetExpenseById(expenseEventId);
            var baseExpense = await _baseExpenseRepository.GetBaseExpenseById(expense.BaseExpenseId);

            foreach (var item in baseExpense.Expenses)
            {
                if (item.DueDate > expense.DueDate )
                {
                    _expenseRepository.DeleteExpense(item);
                }
            }

            // Deletes base expense class when there are no expenses belonging to it
            if (expense.BaseExpense.Expenses.Count <= 1)
            {
                _baseExpenseRepository.DeleteBaseExpense(baseExpense);
            }
            await _baseExpenseRepository.SaveAsync();
        }

        public async Task DeleteExpense(int expenseEventId)
        {
            var expense = await _expenseRepository.GetExpenseById(expenseEventId);
            var baseExpense = await _baseExpenseRepository.GetBaseExpenseById(expense.BaseExpenseId);

            _expenseRepository.DeleteExpense(expense);
            
            // Deletes base expense class when there are no expenses belonging to it
            if (expense.BaseExpense.Expenses.Count <= 1)
            {
                _baseExpenseRepository.DeleteBaseExpense(baseExpense);
            }
            
            await _expenseRepository.SaveAsync();
        }

        public async Task<ExpenseVm> GetExpense(int expenseEventId)
        {
            var expense = await _expenseRepository.GetExpenseById(expenseEventId);
            return _mapper.Map<ExpenseVm>(expense);
        }

        public async Task UpdateExpense(ExpenseEditVm expenseEditVm)
        {
            var expense = _mapper.Map<Expense>(expenseEditVm);
            _expenseRepository.UpdateExpense(expense);
            _baseExpenseRepository.Save();
            await _baseExpenseRepository.SaveAsync();
        }

        public Task UpdateRecurringExpenses(ExpenseVm expenseVm)
        {
            throw new NotImplementedException();
        }

        public List<CategoryVm> GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            return _mapper.Map<List<CategoryVm>>(categories);
        }

        private async Task CreateSingleExpense(ExpenseCreateVm expenseCreateVm)
        {
            List<Expense> expenses = new List<Expense>();
            expenses.Add(new Expense
            {
                BuyerId = expenseCreateVm.BuyerId,
                DueDate = expenseCreateVm.DueDate,
                Price = expenseCreateVm.Price,
                CurrencyId = 1,
                PaymentType = expenseCreateVm.PaymentType,
                ExpenseId = expenseCreateVm.ExpenseId
            });

            var baseExpense = _mapper.Map<BaseExpense>(expenseCreateVm);
            baseExpense.Expenses = expenses;
            _baseExpenseRepository.InsertBaseExpense(baseExpense);
            await _baseExpenseRepository.SaveAsync();
        }
        private async Task CreateRecurringExpense(ExpenseCreateVm expenseCreateVm)
        {
            List<Expense> expenses = new List<Expense>();

            var dueDate = expenseCreateVm.DueDate;
            const int recurringExpenseMaxYear = 3;
            var endDate = dueDate.AddYears(recurringExpenseMaxYear);

            while (dueDate <= endDate)
            {
                dueDate = NextDueDate(dueDate, expenseCreateVm.Frequency);

                if (dueDate > endDate)
                {
                    break;
                }

                expenses.Add(new Expense
                {
                    BuyerId = expenseCreateVm.BuyerId,
                    DueDate = dueDate,
                    Price = expenseCreateVm.Price,
                    PaymentType = expenseCreateVm.PaymentType,
                    ExpenseId = expenseCreateVm.ExpenseId,
                    CurrencyId = 1
                });
            }
            var baseExpense = _mapper.Map<BaseExpense>(expenseCreateVm);
            baseExpense.Expenses = expenses;
            _baseExpenseRepository.InsertBaseExpense(baseExpense);
            await _baseExpenseRepository.SaveAsync();
        }
        private DateTime NextDueDate(DateTime currDueDate, Frequency frequency)
        {
            var dateTime = currDueDate;

            switch (frequency)
            {
                case Frequency.Weekly:
                    return dateTime.AddDays(7);
                case Frequency.Monthly:
                    return dateTime.AddMonths(1);
                case Frequency.Bimonthly:
                    return dateTime.AddMonths(2);
                case Frequency.Semesterly:
                    return dateTime.AddMonths(6);
                case Frequency.Yearly:
                    return dateTime.AddYears(1);
            }
            return dateTime;
        }
    }
}