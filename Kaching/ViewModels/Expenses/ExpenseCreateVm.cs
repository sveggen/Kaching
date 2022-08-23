using System.ComponentModel.DataAnnotations.Schema;
using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpenseCreateVm
    {
        public decimal Price { get; set; }

        public int CreatorId { get; set; }

        public int ExpenseId { get; set; }

        public Category? Category { get; set; }

        public int BuyerId { get; set; }

        public string? Comment { get; set; } = string.Empty;

        public PaymentType PaymentType { get; set; }

        public DateTime PaymentDate { get; set; }
        
        public Frequency Frequency { get; set; }
    }
}
