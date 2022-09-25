using FluentEmail.Core;
using Microsoft.AspNetCore.Identity.UI.Services;

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
    }
}
