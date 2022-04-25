using System.ComponentModel.DataAnnotations.Schema;
using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpenseViewModel
    {
        public int ExpenseId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int CreatorId { get; set; }

        public PersonViewModel? Creator { get; set; }

        public Category? Category { get; set; }

        public string? Description { get; set; } = string.Empty;

        public int BuyerId { get; set; }

        public PersonViewModel? Buyer { get; set; }

        public PaymentStatus? PaymentStatus { get; set; }

        public PaymentType? PaymentType { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
