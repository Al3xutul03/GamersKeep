namespace Repository.Entity;

public class CartEntry
{
    public ulong UserId { get; set; }
    public User? User { get; set; }
    public ulong ProductId { get; set; }
    public Product? Product { get; set; }
    public uint Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
