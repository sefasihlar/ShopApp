using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace ShopApp.WebUI.MailServices
{
    public class EmailSender:IEmailSender
    {
        private readonly string SendGridKey = "";

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(SendGridKey, subject, htmlMessage, email);
        }

        private Task Execute(string SendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(SendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("sihlarsefa97@gmail.com", "AiArt"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));


            return client.SendEmailAsync(msg);
        }
    }
}
