using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using ShopApp.WebUI.MailServices;
using System.Net;

public class EmailSender : IEmailSender
{
 
    public AuthMessageSenderOptions Options { get; } // using Microsoft.AspNetCore.Identity.UI.Services;
    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
    {
        Options = optionsAccessor.Value;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Execute(SendGridKey, subject, message, email);
    }

    public Task Execute(string apiKey, string subject, string message, string email)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("test@example.com", "Example User"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(email));
        msg.SetClickTracking(false, false);

        return client.SendEmailAsync(msg);
    }
}