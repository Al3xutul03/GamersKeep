using AutoMapper;
using BusinessLogic.Models.CartEntry;
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
/// <param name="mapper">The mapper used for mapping operations.</param>
/// <param name="cartEntryRepository">The cart entry repository to use.</param>
public class CartService(IMapper mapper, ICartEntryRepository cartEntryRepository)
    : BaseService<(ulong UserId, ulong ProductId), CartEntry, CartEntryReadModel, CartEntryCreateModel, CartEntryUpdateModel>(mapper, cartEntryRepository), ICartService
{
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
        var existing = await CartEntryRepository.GetByIdAsync((userId, productId), IncludeBehaviour.NoInclude);
        var now = DateTime.UtcNow;

        if (existing != null)
        {
            existing.Quantity += quantity;
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
}
