using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpenseIndexVM
    {
        public decimal Sum { get; set; }

        public int MonthNumber { get; set; }

        public string MonthName { get; set; }

        public string CurrentDate { get; set; }

        public int Year { get; set; }

        public List<EEventVM>? Expenses { get; set; }

        public List<PersonVM>? Persons { get; set; }
    }
}
