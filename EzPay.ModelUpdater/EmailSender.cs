using System;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

namespace EzPay.ModelUpdater
{
    /// <summary>
    /// Initizes an SMTP instance and sends temporary password by email
    /// </summary>
    public class EmailSender
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class
        /// </summary>
        public EmailSender()
        {
        }

        /// <summary>
        /// Sends the temporary password by email
        /// </summary>
        /// <param name="Email">Email address of the User</param>
        /// <param name="tempPassword">Temporary password to be sent</param>
        public void SendPassword(string Email, string tempPassword)
        {
            var fromEmail = new MailAddress("EZPayVerify@gmail.com", "EZPay Bills");
            var toEmail = new MailAddress(Email);
            string subject = "Your account is successfully created";

            string body = "<br/><br/>Your account is created on EZPay System with your VAT number as User name."
                          + " Your temporary password is " + tempPassword;

            //string EZPayKey = "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";

            var smtp = new SmtpClient
            {
                /*Host = "smtp.sendgrid.net",
                Port = 465,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("EZPayKey", EZPayKey)*/

                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("EZPayVerify", "1q2w3e4r5t6y!@#")
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }
    }
}
