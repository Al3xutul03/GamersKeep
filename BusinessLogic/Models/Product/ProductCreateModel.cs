using Repository.Enums.Types;

namespace BusinessLogic.Models.Product;

/// <summary>
/// Data transfer object for product create information.
/// </summary>
public record ProductCreateModel(
    string Name,
    AgeRatingType AgeRating,
    string Description,
    decimal Price,
    uint Ammount,
    ICollection<CategoryType>? Categories = null,
    ICollection<string>? Images = null);
