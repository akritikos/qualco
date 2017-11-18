namespace EzPay.Model
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EzPay.Model.Entities;
    using EzPay.Model.IdentityEntities;
    using EzPay.Model.Interfaces;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Provides a source-agnostic EzPay model 
    /// </summary>
    public interface IEzPayRepository
    {
        /// <summary>
        /// Clears volatile data from the database: <see cref="Payment"/>, <see cref="Bill"/>, <see cref="Settlement"/>
        /// </summary>
        void ClearVolatile();

        /// <summary>
        /// Returns a DataSet of entities allowing r/w operations
        /// </summary>
        /// <typeparam name="T">The Entity type to return</typeparam>
        /// <returns>DataSet of <see cref="T"/> currently in the database</returns>
        DbSet<T> GetSet<T>() where T : class, IEntity;

        /// <summary>
        /// Save pending changes for tracked objects
        /// </summary>
        /// <returns>Save success</returns>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Save pending changes for tracked objects
        /// </summary>
        /// <returns>Save success</returns>
        bool SaveChanges();

        /// <summary>
        /// Adds an object to the currently tracked entities
        /// </summary>
        /// <param name="entity">The entity to start tracking for</param>
        void Add(IEntity entity);

        /// <summary>
        /// Starts tracking a collection of entities
        /// </summary>
        /// <param name="entities">The entities to be tracked</param>
        void AddRange(IEnumerable<IEntity> entities);

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">The entity to remove</param>
        void Remove(IEntity entity);

        /// <summary>
        /// Deletes and stops tracking a collection of entities
        /// </summary>
        /// <param name="entities">The entities to be removed</param>
        void RemoveRange(IEnumerable<IEntity> entities);

        /// <summary>
        /// Checks if repositry is up to date and contactable
        /// </summary>
        /// <returns>Is repository healthy</returns>
        bool CheckContext();
    }
}
