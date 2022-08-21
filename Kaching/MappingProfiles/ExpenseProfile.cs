using AutoMapper;
using Kaching.Models;
using Kaching.ViewModels;

namespace Kaching.MappingProfiles
{
    public class ExpenseProfile : Profile
    {

        public ExpenseProfile()
        {
            CreateMap<BaseExpense, ExpenseVm>();

            CreateMap<ExpenseVm, ExpensesByMonthVm>();
            CreateMap<PersonLightVm, ExpensesByMonthVm>();

            CreateMap<ExpensesByMonthVm, PersonLightVm>();

            CreateMap<ExpenseVm, Expense>();
            CreateMap<Expense, ExpenseVm>();
            CreateMap<ExpenseVm, ExpensesByMonthVm>();

            CreateMap<ExpenseCreateVM, BaseExpense>()
                .ForMember(m => m.Frequency, d => d.MapFrom(k => Frequency.Once))
                .ForMember(m => m.StartDate, d => d.MapFrom(k => k.PaymentDate))
                .ForMember(m => m.EndDate, d => d.MapFrom(k => k.PaymentDate));

            CreateMap<ExpenseCreateRecurringVM, BaseExpense>();

            CreateMap<ExpenseEditVm, Expense>();

        }
    }
}
