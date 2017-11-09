namespace EzPay.Model
{
    using EzPay.Model.Entities;
    using EzPay.Model.IdentityEntities;
    using EzPay.Model.Interfaces;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Provides a source-agnostic EzPay model 
    /// </summary>
    public interface IQualcoRepository
    {
        /// <summary>
        /// Clears volatile data from the database: <see cref="Payment"/>, <see cref="Bill"/>, <see cref="Settlement"/>
        /// </summary>
        void ClearVolatile();

        DbSet<T> GetSet<T>() where T : class, IEntity;
    }
}
