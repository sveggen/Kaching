using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles;

public class CurrencyProfile : Profile
{

    public CurrencyProfile()
    {
        CreateMap<Currency, CurrencyVm>();
    }
    
}