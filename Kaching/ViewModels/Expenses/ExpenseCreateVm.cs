using System.ComponentModel.DataAnnotations.Schema;
using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpenseCreateVm
    {
        public decimal Price { get; set; }

        public int GroupId { get; set; }
        
        public int CreatorId { get; set; }

        public int ExpenseId { get; set; }

        public int CategoryId { get; set; }
        
        public int CurrencyId { get; set; }

        public int BuyerId { get; set; }

        public string? Comment { get; set; } = string.Empty;

        public PaymentType PaymentType { get; set; }

        public DateTime PaymentDate { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public Frequency Frequency { get; set; }
    }
}
