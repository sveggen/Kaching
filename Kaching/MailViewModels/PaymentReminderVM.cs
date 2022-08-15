namespace Kaching.MailViewModels
{
    public class PaymentReminderVM
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public decimal OwedAmount { get; set; }

        public string MonthName { get; set; }

        public string PersonToPay { get; set; }

        public DateTime Deadline { get; set; }
    }
}
