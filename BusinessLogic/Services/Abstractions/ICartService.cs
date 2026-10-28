using BusinessLogic.Models.CartEntry;
using BusinessLogic.Services.Generic;
using Repository.Entity;

namespace BusinessLogic.Services.Abstractions;

/// <summary>
/// Cart service interface for managing user shopping cart operations.
/// </summary>
public interface ICartService
    : IBaseService<(ulong UserId, ulong ProductId), CartEntry, CartEntryReadModel, CartEntryCreateModel, CartEntryUpdateModel>
{
}
