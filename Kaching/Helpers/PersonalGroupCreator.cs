using Kaching.Repositories;

namespace Kaching.Helpers;

public class PersonalGroupCreator
{
    private readonly IGroupRepository _groupRepository;

    public PersonalGroupCreator(GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    
    
}