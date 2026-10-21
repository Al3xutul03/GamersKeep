using Repository.Enums.Types;

namespace BusinessLogic.Models;

public record SearchFilterModel(
    string SearchQuery = "",
    AgeRatingType? AgeRating = null,
    CategoryType? Category = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null);
