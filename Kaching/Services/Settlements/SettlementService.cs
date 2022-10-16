using AutoMapper;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services;

public class SettlementService : ISettlementService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ITransferRepository _transferRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public SettlementService(
        IMapper mapper,
        ITransferRepository transferRepository,
        IExpenseRepository expenseRepository,
        IPersonRepository personRepository,
        IGroupRepository groupRepository)
    {
        _mapper = mapper;
        _transferRepository = transferRepository;
        _expenseRepository = expenseRepository;
        _personRepository = personRepository;
        _groupRepository = groupRepository;
    }

    public async Task<SettlementVm> GetAllSettlements(int groupId)
    {
        var group = _groupRepository.GetGroup(groupId);
        return _mapper.Map<SettlementVm>(group);
    }
}