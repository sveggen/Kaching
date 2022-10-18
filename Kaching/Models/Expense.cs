using System.ComponentModel.DataAnnotations.Schema;

namespace Kaching.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        
        public int BaseExpenseId { get; set; } 

        public BaseExpense BaseExpense { get; set; }

        public int BuyerId { get; set; }

        public bool Paid { get; set; }
        
        public Person? Buyer { get; set; }

        public string? Comment { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public int CurrencyId { get; set; }
        
        public Currency Currency { get; set; }

        public PaymentType PaymentType { get; set; }

        public DateTime PaymentDate { get; set; }
        
        public DateTime DueDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }
        
        public int GroupId { get; set; }
        
        public Group? Group { get; set; }
    }
}
