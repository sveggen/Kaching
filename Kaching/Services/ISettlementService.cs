using Kaching.ViewModels;

namespace Kaching.Services;

public interface ISettlementService
{
    public Task<SettlementVm> GetSettlement(int month, int year, int groupId);
    
    public List<SettlementVm> GetAllSettlements(int groupId);
}