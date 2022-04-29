using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpenseIndexViewModel
    {
        public decimal Sum { get; set; }

        public int MonthNumber { get; set; }

        public string MonthName { get; set; }

        public string CurrentDate { get; set; }

        public int Year { get; set; }

        public List<ExpenseEventViewModel>? Expenses { get; set; }

        public List<PersonViewModel>? Persons { get; set; }
    }
}
