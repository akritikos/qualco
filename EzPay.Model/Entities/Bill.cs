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
        /// Identifier of possible <see cref="Entities.Settlement"/>
        /// </summary>
        public Guid? SettlementId { get; set; }

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
        public decimal Amount { get; set; }

        /// <summary>
        /// Navigational property to access the owning <see cref="Entities.Citizen"/>
        /// </summary>
        public Citizen Citizen { get; set; }

        /// <summary>
        /// Navigational property to access a sucessful transaction <see cref="Entities.Payment"/>
        /// </summary>
        public Payment Payment { get; set; }

        /// <summary>
        /// Navigational property to access possible <see cref="Entities.Settlement"/>
        /// </summary>
        public Settlement Settlement { get; set; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Bill))
            {
                return false;
            }
            var o = (Bill)obj;
            return o.Id.Equals(Id);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Id.GetHashCode();
    }
}
