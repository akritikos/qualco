namespace EzPay.Model.Comparer
{
    using System.Collections.Generic;

    using EzPay.Model.Entities;

    /// <inheritdoc />
    /// <summary>
    /// Handles comparisons on <see cref="Bill"/> objects based on their ID
    /// </summary>
    public class BillComparer : IComparer<Bill>
    {
        /// <inheritdoc />
        public int Compare(Bill x, Bill y) => x.Id.CompareTo(y.Id);
    }
}
