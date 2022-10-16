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

            CreateMap<Expense, ExpenseVm>()
                .ForMember(m
                    => m.CategoryName, d
                    => d.MapFrom(k
                        => k.BaseExpense.Category.Name))
                .ForMember(m
                    => m.CategoryIcon, d
                    => d.MapFrom(k
                        => k.BaseExpense.Category.Icon))
                .ForMember(m
                    => m.CategoryId, d
                    => d.MapFrom(k
                        => k.BaseExpense.Category.CategoryId))
                .ForMember(m
                    => m.BaseExpenseId, d
                    => d.MapFrom(k
                        => k.BaseExpense.BaseExpenseId))
                .ForMember(m
                    => m.Frequency, d
                    => d.MapFrom(k
                        => k.BaseExpense.Frequency));

            CreateMap<Expense, PersonLightVm>();
            
            CreateMap<ExpenseVm, ExpensesByMonthVm>();

            CreateMap<ExpenseCreateVm, BaseExpense>()
                .ForMember(m
                    => m.StartDate, d
                    => d.MapFrom(k
                    => k.PaymentDate))
                .ForMember(m
                    => m.EndDate, d
                    => d.MapFrom(k
                    => k.PaymentDate));

            CreateMap<ExpenseEditVm, Expense>();

            CreateMap<Category, CategoryVm>();

        }
    }
}
