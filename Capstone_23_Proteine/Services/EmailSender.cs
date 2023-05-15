using SendGrid;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;


namespace Capstone_23_Proteine.Services
{
    public class EmailSender
    {
        public async Task SendEmail(string subject, string email, string message)
        {
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("proteine.team@gmail.com", "Proteine Team");
            var to = new EmailAddress(email);
            var plaintextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plaintextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

        }
    }
}
