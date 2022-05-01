using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class PaymentProfile : Profile
    {

        public PaymentProfile()
        {
            CreateMap<PaymentCreateViewModel, Payment>()
                .ForMember(m => m.Created, conf => conf.MapFrom(ml => DateTime.Now));

        }
    }
}
