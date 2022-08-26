using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class TransferProfile : Profile
    {

        public TransferProfile()
        {
            CreateMap<TransferCreateVM, Transfer>()
                .ForMember(m => m.Created, conf
                    => conf.MapFrom(ml => DateTime.Now));
            CreateMap<Transfer, TransferVm>()
                .ForMember(e => e.PaymentMonthYear, f =>
                    f.MapFrom(k => k.PaymentMonth));
        }
    }
}
