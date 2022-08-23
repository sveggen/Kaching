using Kaching.Models;

namespace Kaching.Repositories;

public interface IGroupRepository
{
    public Task<List<Group>> GetGroups();

    public Group GetGroup(int groupId);
    
    public void InsertGroup(Group group);

    public void DeleteGroup(Group group);

    public void AddMember(Group group);
    
    // remove member 
    
    // edit group
    
    public void Save();

    public Task SaveAsync();
}