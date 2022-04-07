using System.ComponentModel.DataAnnotations.Schema;

namespace Kaching.Models
{
    public class Expense
    {

        public int ExpenseId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Other person owe. 
        //public decimal OtherPersonOwe { get; set; } = Price / 2;

        // Paid by
        public int PersonId { get; set; }

        public Person? Person { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string Description { get; set; } = string.Empty;

       // public EventOccurence 

        public PaymentType? PaymentType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }
    }
}
