using Kaching.ViewModels.Settlements;

namespace Kaching.Services;

public interface ISettlementService
{
    public SettlementVm GetSettlement(int month, int groupId);
    
    public List<SettlementVm> GetAllSettlements(int groupId);
}