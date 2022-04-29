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
            CreateMap<ExpenseViewModel, Expense>();

            CreateMap<ExpenseViewModel, ExpenseIndexViewModel>();
            CreateMap<PersonViewModel, ExpenseIndexViewModel>();

            CreateMap<ExpenseIndexViewModel, PersonViewModel>();

            CreateMap<ExpenseEventViewModel, ExpenseEvent>();
            CreateMap<ExpenseEvent, ExpenseEventViewModel>();
            CreateMap<ExpenseEventViewModel, ExpenseIndexViewModel>();

            CreateMap<ExpenseEventCreateViewModel, Expense>();
            CreateMap<Expense, ExpenseEventCreateViewModel>();
        }
    }
}
