using AutoMapper;
using BusinessLogic.Models.CartEntry;
using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Generic;
using Repository.Entity;
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
}
