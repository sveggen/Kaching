using AutoMapper;
using Kaching.Enums;
using Kaching.Repositories;

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
    
    public void AddGroup(Group group)
    {
        _groupRepository.InsertGroup(group);
    }

    public void AddGroupMember(int groupId, int personId)
    {
        throw new NotImplementedException();
    }
}