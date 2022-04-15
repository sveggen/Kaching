using System.ComponentModel.DataAnnotations.Schema;

namespace Kaching.Models
{
    public class Expense
    {

        public int ExpenseId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int CreatorId { get; set; }

        public Person? Creator { get; set; }

        public Category? Category { get; set; }

        public string? Description { get; set; } = string.Empty;

        // public EventOccurence 

        public int BuyerId { get; set; }

        public Person? Buyer { get; set; }

        public PaymentStatus? PaymentStatus { get; set; }

        public PaymentType? PaymentType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }
    }
}
