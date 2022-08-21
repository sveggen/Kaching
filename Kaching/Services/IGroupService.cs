using Kaching.Enums;

namespace Kaching.Services;

public interface IGroupService
{
    public void AddGroup(Group group);

    public void AddGroupMember(int groupId, int personId);
}