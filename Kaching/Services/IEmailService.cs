using Kaching.MailViewModels;

namespace Kaching.Services
{
    public interface IEmailService
    {

        public Task SendPaymentReminder(PaymentReminderViewModel paymentVM);

    }
}
