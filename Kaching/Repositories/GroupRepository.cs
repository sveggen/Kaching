using Kaching.Data;
using Kaching.Enums;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Repositories;

public class GroupRepository : IGroupRepository
{
    
    private readonly DataContext _context;

    public GroupRepository(DataContext context)
    {
        _context = context;
    }
    
    public Task<List<Group>> GetGroups()
    {
        return _context.Group.ToListAsync();
    }

    public Group GetGroup(int groupId)
    {
        return _context.Group.FirstOrDefault(x => x.GroupId == groupId);
    }

    public void InsertGroup(Group group)
    {
        _context.Group.Add(group);
    }

    public void DeleteGroup(Group group)
    {
        _context.Group.Remove(group);
    }

    public void AddMember(Group group)
    {
        _context.Group.Update(group);
    }
}