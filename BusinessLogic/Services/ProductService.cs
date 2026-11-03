using AutoMapper;
using BusinessLogic.Enums.Types;
using BusinessLogic.Models;
using BusinessLogic.Models.Product;
using BusinessLogic.Services.Abstractions;
using BusinessLogic.Services.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Abstractions;
using System.Linq.Expressions;

namespace BusinessLogic.Services;

public class ProductService(IMapper mapper, IProductRepository productRepository)
    : BaseService<ulong, Product, ProductReadModel, ProductCreateModel, ProductUpdateModel>(mapper, productRepository), IProductService
{
    private IProductRepository ProductRepository => (IProductRepository) _repository;

    /// <inheritdoc />
    public async Task<ulong> GetNoPages(ulong page_size, SearchFilterModel filter)
    {
        var count = await ProductRepository.GetNoPagesAsync(page_size, q =>
            q.Include(p => p.Images)
             .Include(p => p.Categories)
             .Where(CreateFilterExpression(filter)));
        return count;
    }

    /// <inheritdoc />
    public async Task<ICollection<ProductListingModel>> GetProductListingsAsync(ulong page_no, ulong page_size, SearchFilterModel filter)
    {
        var products = await ProductRepository.GetProductPageAsync(page_no, page_size, IncludeBehaviour.GivenIncludes,q =>
            q.Include(p => p.Categories)
             .ThenInclude(pc => pc.Category)
             .Where(CreateFilterExpression(filter)));

        products = filter.SortBy switch
        {
            SortByType.Name => filter.SortDescending ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name),
            SortByType.Price => filter.SortDescending ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price),
            SortByType.Date => filter.SortDescending ? products.OrderByDescending(p => p.CreatedAt) : products.OrderBy(p => p.CreatedAt),
            _ => products
        };

        return [.. products.Select(p => _mapper.Map<ProductListingModel>(p))];
    }

    private static Expression<Func<Product, bool>> CreateFilterExpression(SearchFilterModel filter)
    {
        return p => (string.IsNullOrEmpty(filter.SearchQuery) || p.Name.Contains(filter.SearchQuery)) &&
                    (!filter.AgeRating.HasValue || p.AgeRating == filter.AgeRating.Value) &&
                    (!filter.Category.HasValue || p.Categories!.Any(c => c.CategoryId == filter.Category.Value)) &&
                    (!filter.MinPrice.HasValue || p.Price >= filter.MinPrice.Value) &&
                    (!filter.MaxPrice.HasValue || p.Price <= filter.MaxPrice.Value) &&
                    (filter.IncludeOutOfStock || p.Stock > 0);
    }
}
