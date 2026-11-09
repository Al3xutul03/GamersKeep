namespace Application.Models;

public class CartItemViewModel
{
    public ulong ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
}

public class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; } = new();
    public decimal GrandTotal => Items.Sum(i => i.TotalPrice);
}