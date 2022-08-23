using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.Services;

public interface IGroupService
{
    public void AddGroup(GroupCreateVm groupCreateVm);

    public void AddGroupMember(int groupId, int personId);
}