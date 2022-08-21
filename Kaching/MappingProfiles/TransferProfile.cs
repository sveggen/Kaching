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
                .ForMember(m => m.Created, conf => conf.MapFrom(ml => DateTime.Now));
            CreateMap<Transfer, TransferVm>()
                .ForMember(e => e.SenderUserName, t => t.MapFrom(w => w.Sender.UserName))
                .ForMember(e=> e.ReceiverUserName, t => t.MapFrom(f=> f.Receiver.UserName))
                .ForMember(e => e.PaymentMonth, f => f.MapFrom(k => k.PaymentMonth.ToString("MMMM")));
        }
    }
}
