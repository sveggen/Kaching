﻿namespace Kaching.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        public string ConnectedUserId { get; set; }

        public string ConnectedUserName { get; set; }

        public ICollection<Expense>? ExpensesCreated { get; set; }

        public ICollection<Transfer>? PaymentsReceived { get; set; }

        public ICollection<Transfer>? PaymentsSent { get; set; }

    }
}
