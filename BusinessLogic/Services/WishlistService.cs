using BusinessLogic.Models.Wishlist;
using BusinessLogic.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Abstractions;

namespace BusinessLogic.Services;

public class WishlistService(
    IWishlistRepository wishlistRepository,
    IProductRepository productRepository) : IWishlistService
{
    public async Task<IReadOnlyList<WishlistItemDto>> GetUserWishlistAsync(ulong userId)
    {
        var entries = await wishlistRepository.GetByUserIdAsync(
            userId,
            IncludeBehaviour.GivenIncludes,
            q => q.Include(w => w.Product).ThenInclude(p => p!.Images));

        return entries
            .Where(w => w.Product != null && w.Product.DeletedAt == null)
            .OrderByDescending(w => w.CreatedAt)
            .Select(w => new WishlistItemDto(
                w.ProductId,
                w.Product!.Name,
                w.Product.Price,
                w.Product.Stock,
                w.Product.Images.Select(i => i.ImageUrl).FirstOrDefault() ?? string.Empty,
                w.CreatedAt))
            .ToList();
    }

    public async Task<HashSet<ulong>> GetProductIdsAsync(ulong userId)
    {
        var entries = await wishlistRepository.GetByUserIdAsync(userId);
        return entries.Select(w => w.ProductId).ToHashSet();
    }

    public async Task<bool> ToggleAsync(ulong userId, ulong productId)
    {
        var existing = await wishlistRepository.GetByIdAsync((userId, productId), IncludeBehaviour.NoInclude);
        if (existing != null)
        {
            wishlistRepository.Delete(existing);
            await wishlistRepository.SaveAsync();
            return false;
        }

        var product = await productRepository.GetByIdAsync(productId, IncludeBehaviour.NoInclude);
        if (product == null || product.DeletedAt != null)
        {
            throw new KeyNotFoundException($"Produsul cu ID-ul {productId} nu a fost găsit.");
        }

        await wishlistRepository.AddAsync(new WishlistEntry
        {
            UserId = userId,
            ProductId = productId,
            CreatedAt = DateTime.UtcNow
        });
        await wishlistRepository.SaveAsync();
        return true;
    }
}