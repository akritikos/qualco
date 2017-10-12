﻿namespace EzPay.Model.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Mapper for SettlementTypes
    /// </summary>
    public class SettlementType
    {
        /// <summary>
        /// Auto-generated identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Percentage of amount that should be paid upfront
        /// </summary>
        public double Downpayment { get; set; }

        /// <summary>
        /// Interest per annum
        /// </summary>
        public double Interest { get; set; }

        /// <summary>
        /// Maximum number of allowed installments
        /// </summary>
        public int MaxInstallments { get; set; }

        /// <summary>
        /// Navigational field mapping <see cref="Settlement"/> classes referring to this <see cref="SettlementType"/>
        /// </summary>
        public virtual ICollection<Settlement> Settlements { get; set; }
    }
}
