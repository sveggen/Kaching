using Kaching.Data;
using Kaching.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Repositories;

public class GroupRepository : IGroupRepository
{
    
    private readonly DataContext _context;

    public GroupRepository(DataContext context)
    {
        _context = context;
    }
    
    public List<Group> GetGroups()
    {
        return _context.Group.ToList();
    }

    public List<Group> GetPersonsGroups(Person person)
    {
        return _context.Group
            .Include(x => x.Members)
            .Where(x => x.Members.Contains(person))
            .ToList();
    }

    public Group GetGroup(int groupId)
    {
        return _context.Group.FirstOrDefault(x => x.GroupId == groupId);
    }

    public Group GetPersonalGroup(int personId)
    {
        return _context.Group.FirstOrDefault
            (x => x.Members.FirstOrDefault().PersonId == personId && x.Personal == true);
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

    public void Save()
    {
        _context.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}