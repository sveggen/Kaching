namespace Kaching.ViewModels;

public class SettlementVm
{
    public int SettlementId { get; set; }
    
    public List<PersonVm> GroupMembers { get; set; }
    
    public int TotalAmount { get; set; }
}