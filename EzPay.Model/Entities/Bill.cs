namespace EzPay.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Bill mapper class
    /// </summary>
    public class Bill
    {
        /// <summary>
        /// Auto-generated unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identifier of <see cref="Entities.Citizen"/> owning this <see cref="Bill"/>
        /// </summary>
        public long CitizenId { get; set; }

        /// <summary>
        /// Date (in YYYYMMDD format) of expiration
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Short description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Amount to be paid
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Navigational property to access the owning <see cref="Entities.Citizen"/>
        /// </summary>
        public Citizen Citizen { get; set; }

        /// <summary>
        /// Navigational property to access a sucessful transaction <see cref="Entities.Payment"/>
        /// </summary>
        public virtual Payment Payment { get; set; }

        /// <summary>
        /// Navigational mapping between <see cref="Bill"/> and agreed <see cref="Settlement"/>
        /// </summary>
        public ICollection<SettledBills> Settled { get; set; }
    }
}
