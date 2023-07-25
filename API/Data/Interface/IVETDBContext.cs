using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace VETAPPAPI.Data.Interface
{
    public interface IVETDBContext : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets to sets the change tracker.
        /// </summary>
        ChangeTracker ChangeTracker { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the DB set for a specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>The DB set.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Gets an entry from an entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The entity entry/</returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion Methods
    }
}
