namespace Kaching.Models
{
    public class Payment
    {
        public Person? Sender  { get; set; }

        public Person? Receiver { get; set; }

        public decimal Amount { get; set; }

        public DateTime DateTime { get; set; }
    }
}
