using System.ComponentModel.DataAnnotations.Schema;
using Kaching.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaching.ViewModels
{
    [BindProperties]
    public class EEventVM
    {
        public int EEventId { get; set; }

        public int ExpenseId { get; set; }

        public ExpenseVM Expense { get; set; }

        public int BuyerId { get; set; }

        public PersonVM? Buyer { get; set; }

        public string? Comment { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public Frequency Frequency { get; set; }

        public DateTime PaymentDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; set; }
    }
}
