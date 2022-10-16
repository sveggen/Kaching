namespace Kaching.ViewModels;

public class SettlementVm
{
    public int SettlementId { get; set; }
     
    public int GroupId { get; set; }

    public List<PersonVm>? Members { get; set; }

    public List<ExpenseVm>? Expenses { get; set; }
        
    public List<TransferVm>? Transfers { get; set; }
}