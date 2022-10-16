using AutoMapper;
using Kaching.Models;
using Kaching.Repositories;
using Kaching.ViewModels;

namespace Kaching.Services;

public class GroupService : IGroupService
{

    private readonly IGroupRepository _groupRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GroupService(
        IGroupRepository groupRepository,
        IPersonRepository personRepository,
        IMapper mapper)
    {
        _groupRepository = groupRepository;
        _personRepository = personRepository;
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

    public List<GroupVm> GetPersonsGroups(int personId)
    {
        var person = _personRepository.GetPersonByPersonId(personId);
        var groups = _groupRepository.GetPersonsGroups(person);
        return _mapper.Map<List<GroupVm>>(groups);
    }

    public void AddGroupMember(int groupId, int personId)
    {
        throw new NotImplementedException();
    }

    public void DeleteGroup(int groupId)
    {
        var group = _groupRepository.GetGroup(groupId);
        _groupRepository.DeleteGroup(group);
        _groupRepository.Save();
    }
}