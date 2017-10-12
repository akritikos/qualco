namespace EzPay.Model.Comparer
{
    using System.Collections.Generic;

    using EzPay.Model.Entities;

    /// <inheritdoc />
    public class CitizenComparerById : IEqualityComparer<Citizen>
    {
        /// <inheritdoc />
        public bool Equals(Citizen x, Citizen y) => x.Id == y.Id;

        /// <inheritdoc />
        public int GetHashCode(Citizen obj) => obj.Id.GetHashCode();
    }
}
