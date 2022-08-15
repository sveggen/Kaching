namespace Kaching.ViewModels
{
    public class TransferVM
    {
        public string? SenderUserName { get; set; }

        public string? ReceiverUserName { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public string? PaymentMonth { get; set; }

        public DateTime Created { get; set; }
    }
}
