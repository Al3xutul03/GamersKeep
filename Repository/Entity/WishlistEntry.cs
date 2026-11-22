namespace Repository.Entity;

public class WishlistEntry
{
    public ulong UserId { get; set; }
    public User? User { get; set; }
    public ulong ProductId { get; set; }
    public Product? Product { get; set; }
    public DateTime CreatedAt { get; set; }
}