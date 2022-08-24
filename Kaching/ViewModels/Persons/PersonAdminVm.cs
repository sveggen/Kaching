using System.ComponentModel;

namespace Kaching.ViewModels;

public class PersonAdminVm
{
    public int PersonId { get; set; }

    [DisplayName("Username")]
    public string? UserName { get; set; }
        
    public string? Avatar { get; set; }

    public string? ColorCode { get; set; }
    
    public string? UserId { get; set; }
    
    public string? Email { get; set; }
    
    public string? Role { get; set; }
}