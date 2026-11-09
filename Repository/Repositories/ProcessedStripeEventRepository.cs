using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Repositories.Abstractions;
using Repository.Repositories.Generic;

namespace Repository.Repositories;

/// <summary>
/// Repository implementation for Stripe webhook events that have already been handled.
/// </summary>
/// <param name="context">The database context to use.</param>
public class ProcessedStripeEventRepository(DbContext context)
    : BaseRepository<string, ProcessedStripeEvent>(context, "EventId"), IProcessedStripeEventRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsAsync(string eventId)
    {
        return await _dbSet.AnyAsync(e => e.EventId == eventId);
    }
}
