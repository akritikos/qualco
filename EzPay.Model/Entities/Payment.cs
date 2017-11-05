namespace EzPay.Model.Entities
{
    using System;

    using EzPay.Model.Interfaces;

    /// <summary>
    /// Payment mapper class
    /// </summary>
    public sealed class Payment : IEntity
    {
        /// <summary>
        /// Unique Identifier of bill being paid by this transaction
        /// </summary>
        public Guid BillId { get; set; }

        /// <summary>
        /// Date and Time (should be in UTC format) of transaction
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Method use to pay transaction
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Navigational property to access <see cref="Entities.Bill"/> referred to by <see cref="BillId"/>
        /// </summary>
        public Bill Bill { get; set; }
    }
}
