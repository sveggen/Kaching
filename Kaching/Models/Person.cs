using System.Drawing;

namespace Kaching.Enums
{
    public class Person
    {
        public int PersonId { get; set; }
        
        public string? Avatar { get; set; }

        public string? ColorCode { get; set; }

        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public ICollection<BaseExpense>? Expenses { get; set; }

        public ICollection<Transfer>? TransfersReceived { get; set; }

        public ICollection<Transfer>? TransfersSent { get; set; }

    }
}
