using Kaching.Models;

namespace Kaching.ViewModels;

public class ExpensePersonalVm
{
    public int ExpenseId { get; set; }

    public decimal Price { get; set; }
        
    public Category? Category { get; set; }

    public bool Paid { get; set; }

    public string? CurrencyName { get; set; }
        
    public string? CurrencySymbol { get; set; }
        
    public PaymentType PaymentType { get; set; }

    public Frequency Frequency { get; set; }

    public DateTime PaymentDate { get; set; }
}