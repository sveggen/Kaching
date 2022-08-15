using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class ExpenseProfile : Profile
    {

        public ExpenseProfile()
        {
            CreateMap<Expense, ExpenseVM>();

            CreateMap<ExpenseVM, ExpenseIndexVM>();
            CreateMap<PersonVM, ExpenseIndexVM>();

            CreateMap<ExpenseIndexVM, PersonVM>();

            CreateMap<EEventVM, EEvent>();
            CreateMap<EEvent, EEventVM>();
            CreateMap<EEventVM, ExpenseIndexVM>();

            CreateMap<ExpenseCreateVM, Expense>()
                .ForMember(m => m.Frequency, d => d.MapFrom(k => Frequency.OneTime))
                .ForMember(m => m.StartDate, d => d.MapFrom(k => k.PaymentDate))
                .ForMember(m => m.EndDate, d => d.MapFrom(k => k.PaymentDate));

            CreateMap<ExpenseCreateRecurringVM, Expense>();

            CreateMap<ExpenseEditVM, EEvent>();

        }
    }
}
