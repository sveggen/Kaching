namespace Kaching.Models
{
    public class ExpenseIndex
    {
        public decimal Sum { get; set; }

        public int MonthNumber { get; set; }

        public int Year { get; set; }

        public List<Person>? Persons { get; set; }

        public List<Expense>? Expenses { get; set; }
    }
}
