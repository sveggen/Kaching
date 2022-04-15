namespace Kaching.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        public string ConnectedUserId { get; set; }

        public string ConnectedUserName { get; set; }

        public List<Expense>? ExpensesCreated { get; set; }

        public List<Expense>? ExpensesPaid { get; set; }

    }
}
