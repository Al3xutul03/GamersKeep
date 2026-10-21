using AutoMapper;
using BusinessLogic.Mapper;
using BusinessLogic.Models;
using BusinessLogic.Models.Product;
using BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Context;
using Repository.Entity;
using Repository.Enums.Types;
using Repository.Repositories;

namespace Tests.Unit.Services;

[TestClass]
[TestCategory("Unit")]
public class ProductServiceTests
{
    private AppDbContext _context = null!;
    private IMapper _mapper = null!;
    private ProductRepository _productRepo = null!;
    private ProductService _productService = null!;

    [TestInitialize]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _productRepo = new ProductRepository(_context);

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));
        var provider = services.BuildServiceProvider();
        _mapper = provider.GetRequiredService<IMapper>();

        _productService = new ProductService(_mapper, _productRepo);

        await SeedTestDataAsync();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private async Task SeedTestDataAsync()
    {
        var catStrategy = new Category { Type = CategoryType.Strategy, Description = "Strategy" };
        var catFamily = new Category { Type = CategoryType.Family, Description = "Family" };
        var catFantasy = new Category { Type = CategoryType.Fantasy, Description = "Fantasy" };
        await _context.Categories.AddRangeAsync(catStrategy, catFamily, catFantasy);

        var p1 = new Product { Id = 1, Name = "Catan", AgeRating = AgeRatingType.SevenPlus, Description = "Classic trading game", Price = 189.99m };
        var p2 = new Product { Id = 2, Name = "Catan: Seafarers", AgeRating = AgeRatingType.SevenPlus, Description = "Expansion for Catan", Price = 140.00m };
        var p3 = new Product { Id = 3, Name = "Gloomhaven", AgeRating = AgeRatingType.SixteenPlus, Description = "Epic fantasy campaign", Price = 699.00m };
        var p4 = new Product { Id = 4, Name = "Terraforming Mars", AgeRating = AgeRatingType.TwelvePlus, Description = "Sci-fi strategy", Price = 299.99m };

        await _context.Products.AddRangeAsync(p1, p2, p3, p4);

        await _context.ProductCategories.AddRangeAsync(
            new ProductCategory { ProductId = 1, CategoryId = CategoryType.Strategy, Product = p1, Category = catStrategy },
            new ProductCategory { ProductId = 1, CategoryId = CategoryType.Family, Product = p1, Category = catFamily },
            new ProductCategory { ProductId = 2, CategoryId = CategoryType.Strategy, Product = p2, Category = catStrategy },
            new ProductCategory { ProductId = 3, CategoryId = CategoryType.Fantasy, Product = p3, Category = catFantasy },
            new ProductCategory { ProductId = 4, CategoryId = CategoryType.Strategy, Product = p4, Category = catStrategy }
        );

        await _context.ProductImages.AddRangeAsync(
            new ProductImage { ProductId = 1, ImageUrl = "https://example.com/catan1.jpg", Product = p1 }
        );

        await _context.SaveChangesAsync();
    }

    [TestMethod]
    public async Task GetNoPages_EmptyFilter_ReturnsTotalPages()
    {
        var filter = new SearchFilterModel();

        var pages = await _productService.GetNoPages(2, filter);

        Assert.AreEqual(2ul, pages);
    }

    [TestMethod]
    public async Task GetNoPages_WithSearchQueryFilter_FiltersByName()
    {
        var filter = new SearchFilterModel(SearchQuery: "Catan");

        var pages = await _productService.GetNoPages(1, filter);

        Assert.AreEqual(2ul, pages);
    }

    [TestMethod]
    public async Task GetProductListingsAsync_FilterByCategory_ReturnsMatchingProducts()
    {
        var filter = new SearchFilterModel(Category: CategoryType.Strategy);

        var listings = await _productService.GetProductListingsAsync(1, 10, filter);

        Assert.HasCount(3, listings);
        Assert.IsTrue(listings.All(l => l.Categories.Contains(CategoryType.Strategy)));
    }

    [TestMethod]
    public async Task GetProductListingsAsync_FilterByAgeRatingAndPriceRange_ReturnsMatchingProducts()
    {
        var filter = new SearchFilterModel(
            AgeRating: AgeRatingType.SevenPlus,
            MinPrice: 150m,
            MaxPrice: 200m);

        var listings = await _productService.GetProductListingsAsync(1, 10, filter);

        Assert.HasCount(1, listings);
        var item = listings.First();
        Assert.AreEqual("Catan", item.Name);
        Assert.AreEqual("https://example.com/catan1.jpg", item.FirstImageUrl);
    }

    [TestMethod]
    public async Task GetProductListingsAsync_Pagination_ReturnsCorrectPage()
    {
        var filter = new SearchFilterModel();

        var page1 = await _productService.GetProductListingsAsync(1, 2, filter);
        var page2 = await _productService.GetProductListingsAsync(2, 2, filter);

        Assert.HasCount(2, page1);
        Assert.HasCount(2, page2);
    }

    [TestMethod]
    public async Task BaseCrud_CreateGetUpdateDelete_WorksEndToEnd()
    {
        var createModel = new ProductCreateModel(
            "Dixit",
            AgeRatingType.SevenPlus,
            "Storytelling card game",
            159.00m);

        await _productService.CreateAsync(createModel);

        var all = await _productService.GetAllAsync();
        var created = all.FirstOrDefault(p => p.Name == "Dixit");
        Assert.IsNotNull(created);
        Assert.AreEqual(159.00m, created!.Price);

        await _productService.DeleteAsync(created.Id);
        var afterDelete = await _productService.GetByIdAsync(created.Id);
        Assert.IsNull(afterDelete);
    }
}
