using Kaching.ViewModels;

namespace Kaching.Services;

public interface ISettlementService
{
    public Task<SettlementVm> GetAllSettlements(int groupId);
}