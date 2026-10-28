using Repository.Enums.Types;

namespace BusinessLogic.Models.Product;

public record ProductListingModel(
    ulong Id,
    string Name,
    AgeRatingType AgeRating,
    string ShortDescription,
    decimal Price,
    uint Ammount,
    ICollection<CategoryType> Categories,
    string FirstImageUrl);