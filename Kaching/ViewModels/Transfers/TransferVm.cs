using System.ComponentModel.DataAnnotations.Schema;

namespace Kaching.ViewModels
{
    public class TransferVm
    {
        public int TransferId { get; set; }

        public int SenderId { get; set; }
        public PersonLightVm? Sender  { get; set; }

        public int ReceiverId { get; set; }

        public PersonLightVm? Receiver { get; set; }
        
        public CurrencyVm? Currency { get; set; }
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
