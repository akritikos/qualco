namespace EzPay.Model.Comparer
{
    using System.Collections.Generic;

    using EzPay.Model.Entities;

    /// <inheritdoc />
    public class BillComparer : IEqualityComparer<Bill>
    {
        /// <inheritdoc />
        public bool Equals(Bill x, Bill y) => x.Id == y.Id;

        /// <inheritdoc />
        public int GetHashCode(Bill obj) => obj.Id.GetHashCode();
    }
}
