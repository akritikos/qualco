namespace EzPay.Services.Utilities
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines services capable of sending emails
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sets parameters for the Email
        /// </summary>
        /// <param name="recipient">To email address</param>
        /// <param name="sender">From email address</param>
        /// <param name="name">Friendly name for sender</param>
        /// <param name="subject">Mail Subject</param>
        /// <param name="bodyText">Content for Mail in HTML format</param>
        /// <param name="bodyHtml">Content for Mail in text format</param>
        void SetParameters(
            string recipient,
            string sender,
            string name,
            string subject,
            string bodyText,
            string bodyHtml);

        /// <summary>
        /// Sends the constructed email
        /// </summary>
        /// <returns>A new Task</returns>
        Task Send();
    }
}
