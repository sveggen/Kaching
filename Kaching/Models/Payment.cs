namespace Kaching.Models
{
    public class Payment
    {
        public Person? Sender  { get; set; }

        public Person? Receiver { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime PaymentPeriod { get; set; }

        public DateTime Created { get; set; }
    }
}
