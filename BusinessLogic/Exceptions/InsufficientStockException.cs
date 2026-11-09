namespace BusinessLogic.Exceptions;

/// <summary>
/// Thrown when an operation cannot be completed because a product has insufficient stock.
/// </summary>
public class InsufficientStockException : Exception
{
    public ulong ProductId { get; }
    public string? ProductName { get; }
    public uint RequestedQuantity { get; }
    public uint AvailableStock { get; }

    public InsufficientStockException(ulong productId, uint requestedQuantity, uint availableStock, string? productName = null)
        : base($"Produsul '{productName ?? productId.ToString()}' nu are stoc suficient (solicitat: {requestedQuantity}, disponibil: {availableStock}).")
    {
        ProductId = productId;
        ProductName = productName;
        RequestedQuantity = requestedQuantity;
        AvailableStock = availableStock;
    }

    public InsufficientStockException(string message) : base(message)
    {
    }

    public InsufficientStockException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
