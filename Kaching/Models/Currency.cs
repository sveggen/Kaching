using System.ComponentModel.DataAnnotations.Schema;

namespace Kaching.Enums;

public class Currency
{
    public int CurrencyId { get; set; }
    
    public string? Name { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public string? NormalizedName { get; set; }
    
    public string? Symbol { get; set; }
    
    public string? Flag { get; set; }
}