using AutoMapper;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services;

public class SettlementService : ISettlementService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ITransferRepository _transferRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public SettlementService(
        IMapper mapper,
        ITransferRepository transferRepository,
        IExpenseRepository expenseRepository,
        IPersonRepository personRepository)
    {
        _mapper = mapper;
        _transferRepository = transferRepository;
        _expenseRepository = expenseRepository;
        _personRepository = personRepository;
    }

    public async Task<SettlementVm> GetSettlement(int month, int year, int groupId)
    {
        var settlement = new SettlementVm
        {
            GroupMembers = _mapper.Map<List<PersonVm>>
                (_personRepository.GetPersonsForSettlement(groupId))
        };

        return settlement;
    }

    public List<SettlementVm> GetAllSettlements(int groupId)
    {
        throw new NotImplementedException();
    }
}