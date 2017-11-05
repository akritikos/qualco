using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Model.Comparer
{
    using EzPay.Model.Entities;

    /// <inheritdoc cref="IComparer{T}" />
    /// <inheritdoc cref="IEqualityComparer{T}" />
    public class BillCompareByDate : IComparer<Bill>, IEqualityComparer<Bill>
    {
        /// <inheritdoc />
        public bool Equals(Bill x, Bill y)
        {
            return x.DueDate.Equals(y.DueDate);
        }

        /// <inheritdoc />
        public int GetHashCode(Bill obj)
        {
            return obj.DueDate.GetHashCode();
        }

        /// <inheritdoc />
        public int Compare(Bill x, Bill y)
        {
            return x.DueDate.CompareTo(y.DueDate);
        }
    }
}
