using Kaching.Data;
using Kaching.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly DataContext _context;

    public CurrencyRepository(DataContext context)
    {
        _context = context;
    }   
    
    public async Task<List<Currency>> GetAllCurrencies()
    {
        return await _context.Currency.ToListAsync();
    }
}