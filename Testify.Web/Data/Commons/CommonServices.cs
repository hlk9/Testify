using SendGrid;
using SendGrid.Helpers.Mail;

namespace Testify.Web.Data.Commons
{
    public class CommonServices
    {
        private readonly string _apiKey;

        public CommonServices(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task sendEmail(List<EmailAddress> listEmailSendTo, string subjectName, string plainTextContent, string htmlTextContent)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("hlk9@proton.me", "Testify Team");
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, listEmailSendTo, subjectName, plainTextContent, htmlTextContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
