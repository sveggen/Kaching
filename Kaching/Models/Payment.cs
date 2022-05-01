using System.ComponentModel.DataAnnotations.Schema;

namespace Kaching.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int SenderId { get; set; }
        public Person? Sender  { get; set; }

        public int ReceiverId { get; set; }

        public Person? Receiver { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime PaymentPeriod { get; set; }

        public DateTime Created { get; set; }
    }
}
