using AutoMapper;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services;

public class GroupService : IGroupService
{

    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GroupService(
        IGroupRepository groupRepository,
        IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }
    
    public void AddGroup(GroupCreateVm groupCreateVm)
    {
        var group = _mapper.Map<Group>(groupCreateVm);
        _groupRepository.InsertGroup(group);
        _groupRepository.Save();
    }

    public List<GroupVm> GetGroups()
    {
        var groups = _groupRepository.GetGroups();
        return _mapper.Map<List<GroupVm>>(groups);
    }

    public void AddGroupMember(int groupId, int personId)
    {
        throw new NotImplementedException();
    }
}