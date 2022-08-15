using FluentEmail.Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using Kaching.MailViewModels;

namespace Kaching.Services
{
    public class EmailSender : IEmailSender
    {
        private IFluentEmail _fluentEmail;

        public EmailSender(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            await _fluentEmail
                .To(to)
                .Subject(subject)
                .Body(message, true)
                .SendAsync();
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

        public async Task SendPaymentReminder(PaymentReminderVM paymentVM)
        {
            var template = 
                "Dear @Model.Username, For the expenses in @Model.MonthName " +
                "You owe @Model.PersonToPay € @Model.OwedAmount. " +
                "Please pay this by @Model.Deadline.";

            await _fluentEmail
                .To(paymentVM.Email)
                .Subject("Kaching - Transfer Reminder for " + paymentVM.MonthName)
                .UsingTemplate(template, paymentVM)
                .SendAsync();
        }
    }
}
