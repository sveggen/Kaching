﻿using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.Services;

public interface IGroupService
{
    public void AddGroup(GroupCreateVm groupCreateVm);
    
    public List<GroupVm> GetGroups();
    
    public List<GroupVm> GetPersonsGroups(int personId);

    public void AddGroupMember(int groupId, int personId);

    public void DeleteGroup(int groupId);
}