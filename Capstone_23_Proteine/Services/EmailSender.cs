using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;


namespace Capstone_23_Proteine.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _sendGridApiKey;

        public EmailSender(string sendGridApiKey)
        {
            _sendGridApiKey = sendGridApiKey;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("proteine.team@gmail.com", "Proteine Team");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
