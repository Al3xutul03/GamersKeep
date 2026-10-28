using Repository.Enums.Types;

namespace BusinessLogic.Models.Product;

/// <summary>
/// Data transfer object for product update information.
/// </summary>
public record ProductUpdateModel(
    ulong Id,
    string Name,
    AgeRatingType AgeRating,
    string Description,
    decimal Price,
    uint Stock,
    ICollection<CategoryType>? Categories = null,
    ICollection<string>? Images = null);
