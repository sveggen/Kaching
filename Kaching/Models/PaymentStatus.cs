using System.ComponentModel.DataAnnotations;

namespace Kaching.Models
{
    public enum PaymentStatus
    {
        Paid,
        [Display(Name = "Not Paid")]
        NotPaid,
        Automatic
    }
}
