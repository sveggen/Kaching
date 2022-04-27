using FluentEmail.Core;
using Kaching.MailViewModels;

namespace Kaching.Services
{
    public class EmailService : IEmailService
    {
        private IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task Send(string to, string body)
        {
            await _fluentEmail.To(to)
                .Body(body).SendAsync();
        }

        public async Task SendRazor(string to, string name)
        {
            var template = "Dear @Model.Name, You are totally @Model.Compliment.";

            await _fluentEmail
                .To(to)
                .Subject("Test email")
                .UsingTemplate(template, new { Name = name, Compliment = "Awesome" })
                .SendAsync();
        }

        public async Task SendPaymentReminder(PaymentReminderViewModel paymentVM)
        {
            var template = 
                "Dear @Model.Username, For the expenses in @Model.MonthName " +
                "You owe @Model.PersonToPay € @Model.OwedAmount. " +
                "Please pay this by @Model.Deadline.";

            await _fluentEmail
                .To(paymentVM.Email)
                .Subject("Kaching - Payment Reminder for " + paymentVM.MonthName)
                .UsingTemplate(template, paymentVM)
                .SendAsync();
        }
    }
}
