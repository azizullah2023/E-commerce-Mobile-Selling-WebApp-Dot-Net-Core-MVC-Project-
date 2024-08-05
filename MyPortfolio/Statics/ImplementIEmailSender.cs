using Microsoft.AspNetCore.Identity.UI.Services;

namespace MyPortfolio.Statics
{
    public class ImplementIEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}
