using System.ComponentModel.DataAnnotations.Schema;
using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpenseCreateRecurringVM
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int CreatorId { get; set; }

        public Category? Category { get; set; }

        public string? Description { get; set; } = string.Empty;

        public int ExpenseId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Frequency Frequency { get; set; }

        public int BuyerId { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }


    }
}
