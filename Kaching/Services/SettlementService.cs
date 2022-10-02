using AutoMapper;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services;

public class SettlementService : ISettlementService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ITransferRepository _transferRepository;
    private readonly IMapper _mapper;

    public SettlementService(
        IMapper mapper,
        ITransferRepository transferRepository,
        IExpenseRepository expenseRepository)
    {
        _mapper = mapper;
        _transferRepository = transferRepository;
        _expenseRepository = expenseRepository;
    }

    public async Task<SettlementVm> GetSettlement(int month, int year, int groupId)
    {
        var expenses = await _expenseRepository
            .GetGroupExpensesByMonth(month, year, groupId);
        var transfers = await _transferRepository
            .GetTransfersByMonthYear(month, year, groupId);

        var settlement = new SettlementVm
        {
            Expenses = _mapper.Map<List<ExpenseVm>>(expenses),
            Transfers = _mapper.Map<List<TransferVm>>(transfers),
        };

        return settlement;
    }

    public List<SettlementVm> GetAllSettlements(int groupId)
    {
        throw new NotImplementedException();
    }
}