using Kaching.Models;

namespace Kaching.ViewModels
{
    public class PaymentCreateViewModel
    {
        public int SenderPersonId { get; set; }

        public int ReceiverPersonId { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public string? PaymentMonth { get; set; }

        public DateTime Created { get; set; }

    }
}
