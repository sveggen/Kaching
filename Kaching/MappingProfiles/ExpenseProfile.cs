using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class ExpenseProfile : Profile
    {

        public ExpenseProfile()
        {
            CreateMap<Expense, ExpenseViewModel>();

            CreateMap<ExpenseViewModel, ExpenseIndexViewModel>();
            CreateMap<PersonViewModel, ExpenseIndexViewModel>();

            CreateMap<ExpenseIndexViewModel, PersonViewModel>();

            CreateMap<ExpenseEventViewModel, ExpenseEvent>();
            CreateMap<ExpenseEvent, ExpenseEventViewModel>();
            CreateMap<ExpenseEventViewModel, ExpenseIndexViewModel>();

            CreateMap<ExpenseCreateVM, Expense>()
                .ForMember(m => m.Frequency, d => d.MapFrom(k => Frequency.OneTime))
                .ForMember(m => m.StartDate, d => d.MapFrom(k => k.PaymentDate))
                .ForMember(m => m.EndDate, d => d.MapFrom(k => k.PaymentDate));

            CreateMap<ExpenseCreateRecurringVM, Expense>();

        }
    }
}
