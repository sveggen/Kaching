using System.ComponentModel.DataAnnotations.Schema;
using Kaching.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kaching.ViewModels
{
    [BindProperties]
    public class ExpenseVm
    {
        public int ExpenseId { get; set; }
        
        public int BaseExpenseId { get; set; }

        public int ResponsibleId { get; set; }

        public decimal Price { get; set; }
        
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        
        public string? CategoryIcon { get; set; }

        public PersonLightVm? Buyer { get; set; }
        
        public int CreatorId { get; set; }
        
        public bool Paid { get; set; }

        public string? Comment { get; set; }

        public string? CurrencyName { get; set; }
        
        public string? CurrencySymbol { get; set; }
        
        public PaymentType PaymentType { get; set; }

        public Frequency Frequency { get; set; }

        public DateTime PaymentDate { get; set; }
        
        public DateTime Updated { get; set; }
    }
}
