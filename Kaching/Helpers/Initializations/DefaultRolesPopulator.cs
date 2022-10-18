using Microsoft.AspNetCore.Identity;

namespace Kaching.Helpers.Initializations;

public class DefaultRolesPopulator
{
    private RoleManager<IdentityRole> _roleManager;
    
    public DefaultRolesPopulator(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public void CreateDefaultRoles()
    {
        _roleManager.CreateAsync(new IdentityRole("Administrator"));
        _roleManager.CreateAsync(new IdentityRole("Regular"));
    }
    
}