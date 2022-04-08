using System.ComponentModel.DataAnnotations;

namespace Kaching.Models
{
    public enum PaymentType
    {
        [Display(Name = "Cash/Card")]
        InStore, 
        SEPA,
        Transfer
    }
}
