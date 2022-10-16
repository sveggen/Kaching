using AutoMapper;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services;

public class SettlementService : ISettlementService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public SettlementService(
        IMapper mapper,
        IGroupRepository groupRepository)
    {
        _mapper = mapper;
        _groupRepository = groupRepository;
    }

    public async Task<SettlementVm> GetAllSettlements(int groupId)
    {
        var group = _groupRepository.GetGroup(groupId);
        return _mapper.Map<SettlementVm>(group);
    }
}