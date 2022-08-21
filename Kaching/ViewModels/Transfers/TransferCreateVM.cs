using System.ComponentModel.DataAnnotations;

namespace Kaching.ViewModels
{
    public class TransferCreateVM
    {
        public int TransferId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime PaymentMonth { get; set; }

        public DateTime Created { get; set; }

        public int GroupId { get; set; }
        
        public string? GroupName { get; set; }
        
        public int? ExpenseId { get; set; }
        
        public ExpenseVm? Expense { get; set; }
    }
}
