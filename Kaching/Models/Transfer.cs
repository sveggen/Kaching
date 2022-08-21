using System.ComponentModel.DataAnnotations.Schema;

namespace Kaching.Enums
{
    public class Transfer
    {
        public int TransferId { get; set; }

        public int SenderId { get; set; }
        public Person? Sender  { get; set; }

        public int ReceiverId { get; set; }

        public Person? Receiver { get; set; }
        
        public int CurrencyId { get; set; }
        
        public Currency? Currency { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime PaymentMonth { get; set; }

        public DateTime Created { get; set; }
        
        public int GroupId { get; set; }
        
        public Group? Group { get; set; }
        
        public int? ExpenseId { get; set; }
        
        public Expense? Expense { get; set; }
    }
}
