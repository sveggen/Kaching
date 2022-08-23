using Kaching.Models;

namespace Kaching.ViewModels
{
    public class ExpenseEditVm
    {
        public int ExpenseId { get; set; }
        
        public decimal Price { get; set; }
        
        public bool Paid { get; set; }

        public Category? Category { get; set; }

        public string? Comment { get; set; }

        public int ResponsibleId { get; set; }

        public PaymentType PaymentType { get; set; }

        public DateTime PaymentDate { get; set; }
        
        public bool EditRecurring { get; set; }
    }
}
