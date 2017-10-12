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
        /// Selected number of installements
        /// (Should be multiples of 3)
        /// </summary>
        public int Installments { get; set; }

        /// <summary>
        /// Navigational property accessing the <see cref="SettlementType"/> of <see cref="TypeId"/>
        /// </summary>
        public SettlementType Type { get; set; }

        /// <summary>
        /// Navigational property mapping the many-to-many relationship between <see cref="Settlement"/> and <see cref="Bill"/>
        /// </summary>
        public virtual ICollection<SettledBills> Settled { get; set; }
    }
}
