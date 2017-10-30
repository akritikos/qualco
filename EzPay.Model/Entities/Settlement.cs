namespace EzPay.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Mapper for Settlement Requests
    /// </summary>
    public class Settlement
    {
        /// <summary>
        /// Auto-generated identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Identifier of <see cref="SettlementType"/> used
        /// </summary>
        public Guid TypeId { get; set; }

        /// <summary>
        /// Identifier of owning <see cref="Citizen"/>
        /// </summary>
        public long CitizenId { get; set; }

        /// <summary>
        /// Date and Time (should be in UTC format) of request
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Selected number of installements
        /// (Should be multiples of 3)
        /// </summary>
        public int Installments { get; set; }

        /// <summary>
        /// Navigational property accessing the <see cref="SettlementType"/> 
        /// of <see cref="SettlementType"/>
        /// </summary>
        public virtual SettlementType Type { get; set; }

        /// <summary>
        /// Navigational property accessing the <see cref="Citizen"/> owning this bill
        /// </summary>
        public virtual Citizen Citizen { get; set; }

        /// <summary>
        /// Navigational property mapping the many-to-many relationship between 
        /// <see cref="Settlement"/> and <see cref="Bill"/>
        /// </summary>
        public virtual ICollection<Bill> Bills { get; set; }
    }
}
