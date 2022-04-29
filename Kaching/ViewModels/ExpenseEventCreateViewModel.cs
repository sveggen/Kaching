using System.ComponentModel.DataAnnotations.Schema;
using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpenseEventCreateViewModel
    {

        public int ExpenseId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int CreatorId { get; set; }

        public PersonViewModel? Creator { get; set; }

        public Category? Category { get; set; }

        public string? Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ExpenseEventId { get; set; }

        public int BuyerId { get; set; }

        public Frequency Frequency { get; set; }

        public PersonViewModel? Buyer { get; set; }

        public string? Comment { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

    }
}
