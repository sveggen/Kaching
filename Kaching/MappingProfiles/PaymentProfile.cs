using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class PaymentProfile : Profile
    {

        public PaymentProfile()
        {
            CreateMap<PaymentCreateVM, Payment>()
                .ForMember(m => m.Created, conf => conf.MapFrom(ml => DateTime.Now));
            CreateMap<Payment, PaymentVM>()
                .ForMember(e => e.SenderUserName, t => t.MapFrom(w => w.Sender.ConnectedUserName))
                .ForMember(e=> e.ReceiverUserName, t => t.MapFrom(f=> f.Receiver.ConnectedUserName))
                .ForMember(e => e.PaymentMonth, f => f.MapFrom(k => k.PaymentPeriod.ToString("MMMM")));
        }
    }
}
