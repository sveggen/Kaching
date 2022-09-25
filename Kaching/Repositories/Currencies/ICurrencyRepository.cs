using Kaching.Models;

namespace Kaching.Repositories;

public interface ICurrencyRepository
{
    public Task<List<Currency>> GetAllCurrencies();
}