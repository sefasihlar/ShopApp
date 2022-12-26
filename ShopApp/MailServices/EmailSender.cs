

// using SendGrid's C# Library
// https://github.com/sendgrid/sendgrid-csharp
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace ShopApp.WebUI.MailServices
{
    public class EmailSender : IEmailSender
    {
        private const string SendGridKey ="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";
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(SendGridKey, subject, htmlMessage, email);
        }

        private Task Execute(string sendGridKey, string subject, string htmlMessage, string email)
        {
            var client = new SendGridClient(sendGridKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@sefasihlar.com", "Shop App"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };

            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}

