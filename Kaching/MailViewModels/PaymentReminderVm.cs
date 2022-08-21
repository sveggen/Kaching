namespace Kaching.MailViewModels
{
    public class PaymentReminderVm
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        public decimal OwedAmount { get; set; }

        public string MonthName { get; set; }

        public string PersonToPay { get; set; }

        public DateTime Deadline { get; set; }
    }
}
