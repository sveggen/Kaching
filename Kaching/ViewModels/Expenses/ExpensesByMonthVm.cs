using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpensesByMonthVm
    {
        public decimal Sum { get; set; }

        public int MonthNumber { get; set; }

        public string? MonthName { get; set; }

        public string? CurrentDate { get; set; }

        public int Year { get; set; }

        public List<ExpenseVm>? Expenses { get; set; }

        public List<PersonLightVm>? Persons { get; set; }
    }
}
