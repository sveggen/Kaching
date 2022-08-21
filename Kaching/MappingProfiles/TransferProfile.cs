using AutoMapper;
using Kaching.Enums;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class TransferProfile : Profile
    {

        public TransferProfile()
        {
            CreateMap<TransferCreateVM, Transfer>()
                .ForMember(m => m.Created, conf => conf.MapFrom(ml => DateTime.Now));
            CreateMap<Transfer, TransferVm>()
                .ForMember(e => e.PaymentMonth, f => f.MapFrom(k => k.PaymentMonth.ToString("MMMM")));
        }
    }
}
