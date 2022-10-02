namespace Kaching.ViewModels.Settlements;

public class SettlementVm
{
    public int SettlementId { get; set; }
    
    public List<PersonVm> Persons { get; set; }
    
    public int TotalAmount { get; set; }
}