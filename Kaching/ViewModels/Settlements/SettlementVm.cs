namespace Kaching.ViewModels;

public class SettlementVm
{
    public int SettlementId { get; set; }
    
    public List<ExpenseVm> Expenses { get; set; }
    
    public List<TransferVm> Transfers { get; set; }
    
    public int TotalAmount { get; set; }
}