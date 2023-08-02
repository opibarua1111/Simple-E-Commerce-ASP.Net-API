using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace simple_ecommerce.api.Services
{
    public class EmailSender
    {
        public async Task SendEmail(string subject, string toEmail, string userName, string message)
        {
            //var apiKey = "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";
            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("opibarua1111@gmail.com", "Simple E-commerce");
            //var to = new EmailAddress(toEmail, userName);
            //var plainTextContent = message;
            //var htmlContent = "";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);
            //Console.WriteLine(response.StatusCode);
            //Console.WriteLine(response.Body.ReadAsStringAsync());

            try
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from_email = new EmailAddress("opibarua1111@gmail.com", "Simple E-commerce");
                var to_email = new EmailAddress(toEmail, userName);
                var plainTextContent = message;
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from_email, to_email, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception e)
            {
               Console.WriteLine( e.Message);
            }
        }
    }
}
