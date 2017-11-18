using System;
using System.Net.Http;
using System.Net.Mail;
using EzPay.Services.Utilities;

namespace EzPay.EmailSender
{
    using System.Data;
    using System.Net.Mime;
    using System.Threading.Tasks;

    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class SendGridSender : IEmailSender
    {
        private readonly SendGridClient client;

        private SendGridMessage msg;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridSender"/> class.
        /// </summary>
        /// <param name="apiKey">
        /// The SendGrid api key to use
        /// </param>
        public SendGridSender(string apiKey)
        {
            client = new SendGridClient(apiKey);
        }

        /// <inheritdoc />
        /// <exception cref="NoNullAllowedException">When Send is requested before setting email parameters
        /// via <see cref="SetParameters"/></exception>
        public async Task Send()
        {
            if (msg == null)
            {
                throw new NoNullAllowedException("You must pass the required values via SetParameters before sending");
            }

            try
            {
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR {ex.GetType().Name}:\n\t{ex.Message}");
            }
        }

        /// <inheritdoc />
        public void SetParameters(string recipient, string sender, string name, string subject, string bodyText, string bodyHtml)
        {
            msg = new SendGridMessage()
                      {
                          From = new EmailAddress(sender, name),
                          Subject = subject,
                          HtmlContent = bodyHtml,
                          PlainTextContent = bodyText
                      };
            msg.AddTo(recipient);
        }
    }
}
