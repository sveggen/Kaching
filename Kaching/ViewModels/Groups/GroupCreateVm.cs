namespace Kaching.ViewModels;

public class GroupCreateVm
{
    public string? Name { get; set; }
        
    public string? Avatar { get; set; }
        
    public string? ColorCode { get; set; }

    public bool? Personal { get; set; }
    
    public int MaxMembers { get; set; }

    public List<PersonLightVm>? Members { get; set; }
    
}