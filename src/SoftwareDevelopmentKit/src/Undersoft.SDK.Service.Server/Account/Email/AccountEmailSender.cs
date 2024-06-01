using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Undersoft.SDK.Service.Server.Accounts.Email;

public class AccountEmailSender : IEmailSender
{
    public AccountEmailSender(IOptions<AccountEmailSenderOptions> optionsAccessor)
    {
        Options = optionsAccessor.Value;
    }

    public AccountEmailSenderOptions Options { get; } //Set with Secret Manager.

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }
        await Execute(Options.SendGridKey, subject, message, toEmail);
    }

    public async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("undersoft@undersoft.pl", "Undersoft"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        msg.SetClickTracking(false, false);
        msg.SetOpenTracking(false);
        msg.SetGoogleAnalytics(false);
        msg.SetSubscriptionTracking(false);

        var response = await client.SendEmailAsync(msg);
        if (response.IsSuccessStatusCode)
            this.Success<Emaillog>($"Email to {toEmail} queued successfully!");
        else
            this.Failure<Emaillog>($"Failure Email to {toEmail}");
    }
}