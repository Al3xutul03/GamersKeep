using Repository.Enums.Types;

namespace BusinessLogic.Models.Product;

/// <summary>
/// Data transfer object for product read information.
/// </summary>
public record ProductReadModel(
    ulong Id,
    string Name,
    AgeRatingType AgeRating,
    string Description,
    decimal Price,
    uint Stock,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    ICollection<CategoryType> Categories,
    ICollection<string> Images);
