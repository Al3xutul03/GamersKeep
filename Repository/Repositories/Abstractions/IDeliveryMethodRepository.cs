using Repository.Entity;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// DeliveryMethod repository interface for the delivery options available at checkout.
/// </summary>
public interface IDeliveryMethodRepository : IBaseRepository<ulong, DeliveryMethod>
{
    /// <summary>
    /// Read all active, non-deleted delivery methods ordered by <see cref="DeliveryMethod.SortOrder"/>.
    /// </summary>
    /// <returns>A collection of selectable delivery methods.</returns>
    public Task<IEnumerable<DeliveryMethod>> GetActiveAsync();
}
