namespace EzPay.Model.Comparer
{
    using System.Collections.Generic;

    using EzPay.Model.Entities;

    /// <inheritdoc />
    /// <summary>
    /// Handles comparisons on <see cref="Citizen"/> objects based on their ID
    /// </summary>
    public class CitizenComparerById : IComparer<Citizen>
    {
        /// <inheritdoc />
        public int Compare(Citizen x, Citizen y) => x.Id.CompareTo(y.Id);
    }
}
