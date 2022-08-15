using System.ComponentModel.DataAnnotations.Schema;

namespace Kaching.Models
{
    public class EEvent
    {
        [Column("ExpenseEventId")]
        public int EEventId { get; set; } 

        public int ExpenseId { get; set; }

        public Expense Expense { get; set; }

        public int BuyerId { get; set; }

        public Person? Buyer { get; set; }

        public string? Comment { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }



    }
}
