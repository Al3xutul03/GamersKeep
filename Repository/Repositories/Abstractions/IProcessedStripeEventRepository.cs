using Repository.Entity;
using Repository.Repositories.Generic;

namespace Repository.Repositories.Abstractions;

/// <summary>
/// Repository interface for Stripe webhook events that have already been handled.
/// </summary>
public interface IProcessedStripeEventRepository : IBaseRepository<string, ProcessedStripeEvent>
{
    /// <summary>
    /// Check whether a Stripe event has already been processed.
    /// </summary>
    /// <param name="eventId">The Stripe event id.</param>
    /// <returns><c>true</c> if the event was already processed.</returns>
    public Task<bool> ExistsAsync(string eventId);
}
