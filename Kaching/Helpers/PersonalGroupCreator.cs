using Kaching.Enums;
using Kaching.Repositories;

namespace Kaching.Helpers;

public class PersonalGroupCreator
{
    private readonly IGroupRepository _groupRepository;

    public PersonalGroupCreator(GroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public void CreatePersonalGroup(Person person)
    {
        var name = person.UserName + "'s Expenses";
        _groupRepository.InsertGroup( new Group
        {
            Name = name,
            MaxMembers = 1,
            Avatar = person.Avatar,
            ColorCode = person.ColorCode
        });
    }
    
    
}