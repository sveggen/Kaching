using Kaching.Models;

namespace Kaching.ViewModels
{
    public class PaymentCreateViewModel
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime PaymentPeriod { get; set; }

        public DateTime Created { get; set; }

    }
}
