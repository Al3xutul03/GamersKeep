using BusinessLogic.Enums.Types;
using Repository.Enums.Types;

namespace BusinessLogic.Models;

public record SearchFilterModel(
    string SearchQuery = "",
    AgeRatingType? AgeRating = null,
    CategoryType? Category = null,
    SortByType SortBy = SortByType.None,
    bool SortDescending = false,
    bool IncludeOutOfStock = false,
    decimal? MinPrice = null,
    decimal? MaxPrice = null);
