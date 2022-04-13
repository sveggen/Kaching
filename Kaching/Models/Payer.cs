namespace Kaching.Models
{
    public class Payer
    {
        public int PayerId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public int PersonId { get; set; }

        public Person? Person { get; set; }

        public Expense? Expense { get; set; }
    }
}
