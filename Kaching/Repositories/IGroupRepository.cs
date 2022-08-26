using Kaching.Models;

namespace Kaching.Repositories;

public interface IGroupRepository
{
    public List<Group> GetGroups();
    
    public List<Group> GetPersonsGroups(Person person);

    public Group GetGroup(int groupId);

    public Group GetPersonalGroup(int personId);
    
    public void InsertGroup(Group group);

    public void DeleteGroup(Group group);

    public void AddMember(Group group);

    // remove member 
    
    // edit group
    
    public void Save();

    public Task SaveAsync();
}