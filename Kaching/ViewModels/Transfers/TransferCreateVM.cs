using System.ComponentModel.DataAnnotations;

namespace Kaching.ViewModels
{
    public class TransferCreateVM
    {
        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        [Range(1, double.MaxValue)]
        public decimal Amount { get; set; }

        [StringLength(150)]
        public string? Description { get; set; }

        public DateTime PaymentPeriod { get; set; }

        public DateTime Created { get; set; }

    }
}
