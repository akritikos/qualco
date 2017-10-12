namespace EzPay.Model.Entities
{
    using System;

    /// <summary>
    /// Mapper of <see cref="Entities.Bill"/> to <see cref="Entities.Settlement"/>
    /// </summary>
    public class SettledBills
    {
        /// <summary>
        /// Identifier of <see cref="Entities.Bill"/>
        /// </summary>
        public Guid BillId { get; set; }

        /// <summary>
        /// Identifier of <see cref="Entities.Settlement"/>
        /// </summary>
        public Guid SettlementId { get; set; }

        /// <summary>
        /// Navigational property accessing the referenced <see cref="Entities.Bill"/>
        /// </summary>
        public Bill Bill { get; set; }

        /// <summary>
        /// Navigational property accessing the referenced <see cref="Entities.Settlement"/>
        /// </summary>
        public Settlement Settlement { get; set; }
    }
}
