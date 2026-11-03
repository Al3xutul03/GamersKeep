using BusinessLogic.Models.DeliveryMethod;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Service managing delivery methods available at checkout.
/// </summary>
public interface IDeliveryMethodService
{
    /// <summary>
    /// Retrieves all active delivery methods ordered for display.
    /// </summary>
    /// <returns>A list of active delivery methods.</returns>
    public Task<IReadOnlyList<DeliveryMethodDto>> GetActiveDeliveryMethodsAsync();

    /// <summary>
    /// Retrieves a delivery method by its ID.
    /// </summary>
    /// <param name="id">The ID of the delivery method.</param>
    /// <returns>The delivery method DTO, or <c>null</c> if not found.</returns>
    public Task<DeliveryMethodDto?> GetByIdAsync(ulong id);
}
