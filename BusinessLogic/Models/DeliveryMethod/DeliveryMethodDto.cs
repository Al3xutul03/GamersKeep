namespace BusinessLogic.Models.DeliveryMethod;

/// <summary>
/// Data transfer object for delivery method information available at checkout.
/// </summary>
public record DeliveryMethodDto(
    ulong Id,
    string Name,
    string Description,
    string EstimatedDelivery,
    decimal Price,
    int SortOrder);
