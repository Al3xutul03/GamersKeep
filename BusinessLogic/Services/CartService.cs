using AutoMapper;
using BusinessLogic.Exceptions;
using BusinessLogic.Models.CartEntry;
using BusinessLogic.Models.Stock;
using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Abstractions;

namespace BusinessLogic.Services;

/// <summary>
/// Business logic service for handling user shopping cart operations.
/// </summary>
public class CartService(
    IMapper mapper,
    ICartEntryRepository cartEntryRepository,
    IProductRepository productRepository)
    : BaseService<(ulong UserId, ulong ProductId), CartEntry, CartEntryReadModel, CartEntryCreateModel, CartEntryUpdateModel>(mapper, cartEntryRepository), ICartService
{
    private readonly IProductRepository _productRepository = productRepository;
    private ICartEntryRepository CartEntryRepository => (ICartEntryRepository)_repository;

    /// <inheritdoc />
    public async Task<ICollection<CartEntryReadModel>> GetUserCartAsync(ulong userId)
    {
        var entries = await CartEntryRepository.GetByUserIdAsync(
            userId,
            IncludeBehaviour.GivenIncludes,
            q => q.Include(ce => ce.Product)
                  .ThenInclude(p => p!.Images)
                  .Include(ce => ce.Product)
                  .ThenInclude(p => p!.Categories));

        return [.. entries.Select(e => _mapper.Map<CartEntryReadModel>(e))];
    }

    /// <inheritdoc />
    public async Task AddOrUpdateItemAsync(ulong userId, ulong productId, uint quantity = 1)
    {
        var product = await _productRepository.GetByIdAsync(productId, IncludeBehaviour.NoInclude)
            ?? throw new KeyNotFoundException($"Produsul cu ID-ul {productId} nu a fost găsit.");

        var existing = await CartEntryRepository.GetByIdAsync((userId, productId), IncludeBehaviour.NoInclude);
        var targetQuantity = (existing?.Quantity ?? 0) + quantity;

        if (targetQuantity > product.Stock)
        {
            throw new InsufficientStockException(productId, targetQuantity, product.Stock, product.Name);
        }

        var now = DateTime.UtcNow;

        if (existing != null)
        {
            existing.Quantity = targetQuantity;
            existing.UpdatedAt = now;
            CartEntryRepository.Update(existing);
        }
        else
        {
            var entry = new CartEntry
            {
                UserId = userId,
                ProductId = productId,
                Quantity = targetQuantity,
                CreatedAt = now,
                UpdatedAt = now
            };
            await CartEntryRepository.AddAsync(entry);
        }

        await CartEntryRepository.SaveAsync();
    }

    /// <inheritdoc />
    public async Task SetQuantityAsync(ulong userId, ulong productId, uint quantity)
    {
        if (quantity == 0)
        {
            try
            {
                await DeleteAsync((userId, productId));
            }
            catch
            {
                // Already deleted or not found
            }
            return;
        }

        var product = await _productRepository.GetByIdAsync(productId, IncludeBehaviour.NoInclude)
            ?? throw new KeyNotFoundException($"Produsul cu ID-ul {productId} nu a fost găsit.");

        if (quantity > product.Stock)
        {
            throw new InsufficientStockException(productId, quantity, product.Stock, product.Name);
        }

        var existing = await CartEntryRepository.GetByIdAsync((userId, productId), IncludeBehaviour.NoInclude);
        var now = DateTime.UtcNow;

        if (existing != null)
        {
            existing.Quantity = quantity;
            existing.UpdatedAt = now;
            CartEntryRepository.Update(existing);
        }
        else
        {
            var entry = new CartEntry
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                CreatedAt = now,
                UpdatedAt = now
            };
            await CartEntryRepository.AddAsync(entry);
        }

        await CartEntryRepository.SaveAsync();
    }

    /// <inheritdoc />
    public async Task ClearCartAsync(ulong userId)
    {
        var entries = (await CartEntryRepository.GetByUserIdAsync(userId)).ToList();
        foreach (var entry in entries)
        {
            CartEntryRepository.Delete(entry);
        }
        await CartEntryRepository.SaveAsync();
    }

    /// <inheritdoc />
    public async Task<StockCheckResult> ValidateCartAsync(ulong userId)
    {
        var entries = (await CartEntryRepository.GetByUserIdAsync(
            userId,
            IncludeBehaviour.GivenIncludes,
            q => q.Include(ce => ce.Product))).ToList();

        if (entries.Count == 0)
        {
            return new StockCheckResult(true, Array.Empty<StockCheckItem>());
        }

        var items = new List<StockCheckItem>();
        var isAllAvailable = true;

        foreach (var entry in entries)
        {
            var product = entry.Product;
            var availableStock = product?.Stock ?? 0;
            var isAvailable = product != null && product.DeletedAt == null && availableStock >= entry.Quantity;

            if (!isAvailable)
            {
                isAllAvailable = false;
            }

            items.Add(new StockCheckItem(
                entry.ProductId,
                product?.Name ?? $"Produs #{entry.ProductId}",
                entry.Quantity,
                availableStock,
                isAvailable));
        }

        return new StockCheckResult(isAllAvailable, items);
    }
}
