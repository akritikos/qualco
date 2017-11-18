using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.EmailSender
{
    using System.Data;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using EzPay.Services.Utilities;
    public class SmtpSender : IEmailSender, IDisposable
    {
        private SmtpClient smtp;

        private MailMessage msg;

        public SmtpSender(string username, string password, string host, int port = 587, bool ssl = false)
        {
            smtp = new SmtpClient()
                            {
                                EnableSsl = ssl,
                                Host = host,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(username, password),
                                Port = port,
                                DeliveryMethod = SmtpDeliveryMethod.Network
                            };
        }

        /// <inheritdoc />
        public void SetParameters(
            string recipient,
            string sender,
            string name,
            string subject,
            string bodyText,
            string bodyHtml) =>
            msg = new MailMessage(sender, recipient)
                      {
                          Subject = subject, Body = bodyHtml, IsBodyHtml = true
                      };

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
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR {ex.GetType().Name}:\n\t{ex.Message}");
            }

            msg.Dispose();
            
        }

        public void Dispose()
        {
            smtp?.Dispose();
            msg?.Dispose();
        }
    }
}
