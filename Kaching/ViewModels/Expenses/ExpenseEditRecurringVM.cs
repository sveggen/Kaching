using System.ComponentModel.DataAnnotations.Schema;
using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpenseEditRecurringVM
    {

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public Category? Category { get; set; }

        public string? Description { get; set; } = string.Empty;

        public int ExpenseId { get; set; }

        public DateTime EndDate { get; set; }

        public int BuyerId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }



    }
}
