

using Microsoft.AspNetCore.Identity;

namespace Kaching.ViewModels
{
    public class PersonVM
    {
        public int PersonId { get; set; }

        public string ConnectedUserId { get; set; }

        public string ConnectedUserName { get; set; }

        public decimal SumOfExpenses { get; set; }

        public decimal OwesOwed { get; set; }

        public decimal PaymentsReceived { get; set; }

        public decimal PaymentsSent { get; set; }

    }
}
