using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Context;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Enums.Types;
using Repository.Repositories;

namespace Tests.Unit.Repositories;

[TestClass]
[TestCategory("Unit")]
public class ProductRepositoryTests
{
    private AppDbContext _context = null!;
    private ProductRepository _productRepo = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _productRepo = new ProductRepository(_context);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    private async Task SeedSampleProductsAsync()
    {
        var catStrategy = new Category { Type = CategoryType.Strategy, Description = "Strategy" };
        var catFamily = new Category { Type = CategoryType.Family, Description = "Family" };
        await _context.Categories.AddRangeAsync(catStrategy, catFamily);

        var products = new List<Product>
        {
            new() { Id = 1, Name = "Catan", AgeRating = AgeRatingType.SevenPlus, Description = "Desc 1", Price = 150m },
            new() { Id = 2, Name = "Carcassonne", AgeRating = AgeRatingType.SevenPlus, Description = "Desc 2", Price = 120m },
            new() { Id = 3, Name = "Terraforming Mars", AgeRating = AgeRatingType.TwelvePlus, Description = "Desc 3", Price = 280m },
            new() { Id = 4, Name = "Gloomhaven", AgeRating = AgeRatingType.SixteenPlus, Description = "Desc 4", Price = 650m },
            new() { Id = 5, Name = "Pandemic", AgeRating = AgeRatingType.SevenPlus, Description = "Desc 5", Price = 170m }
        };

        await _context.Products.AddRangeAsync(products);
        await _context.ProductCategories.AddRangeAsync(
            new ProductCategory { ProductId = 1, CategoryId = CategoryType.Strategy, Category = catStrategy },
            new ProductCategory { ProductId = 1, CategoryId = CategoryType.Family, Category = catFamily },
            new ProductCategory { ProductId = 3, CategoryId = CategoryType.Strategy, Category = catStrategy }
        );
        await _context.SaveChangesAsync();
    }

    [TestMethod]
    public async Task GetNoProductsAsync_WithoutFilter_ReturnsTotalCount()
    {
        await SeedSampleProductsAsync();

        var count = await _productRepo.GetNoProductsAsync();

        Assert.AreEqual(5ul, count);
    }

    [TestMethod]
    public async Task GetNoProductsAsync_WithFilter_ReturnsFilteredCount()
    {
        await SeedSampleProductsAsync();

        var count = await _productRepo.GetNoProductsAsync(q => q.Where(p => p.Price > 200m));

        Assert.AreEqual(2ul, count);
    }

    [TestMethod]
    public async Task GetNoPagesAsync_CalculatesCeilingCorrectly()
    {
        await SeedSampleProductsAsync();

        var pagesSize2 = await _productRepo.GetNoPagesAsync(2);
        var pagesSize5 = await _productRepo.GetNoPagesAsync(5);
        var pagesSize10 = await _productRepo.GetNoPagesAsync(10);

        Assert.AreEqual(3ul, pagesSize2);
        Assert.AreEqual(1ul, pagesSize5);
        Assert.AreEqual(1ul, pagesSize10);
    }

    [TestMethod]
    public async Task GetProductPageAsync_ReturnsCorrectPageSlice()
    {
        await SeedSampleProductsAsync();

        var page1 = (await _productRepo.GetProductPageAsync(1, 2, IncludeBehaviour.NoInclude, q => q.OrderBy(p => p.Id))).ToList();
        var page2 = (await _productRepo.GetProductPageAsync(2, 2, IncludeBehaviour.NoInclude, q => q.OrderBy(p => p.Id))).ToList();
        var page3 = (await _productRepo.GetProductPageAsync(3, 2, IncludeBehaviour.NoInclude, q => q.OrderBy(p => p.Id))).ToList();

        Assert.HasCount(2, page1);
        Assert.AreEqual(1ul, page1[0].Id);
        Assert.AreEqual(2ul, page1[1].Id);

        Assert.HasCount(2, page2);
        Assert.AreEqual(3ul, page2[0].Id);
        Assert.AreEqual(4ul, page2[1].Id);

        Assert.HasCount(1, page3);
        Assert.AreEqual(5ul, page3[0].Id);
    }

    [TestMethod]
    public async Task BasicCrud_AddGetUpdateDelete_WorksCorrectly()
    {
        var product = new Product
        {
            Id = 100,
            Name = "Root",
            AgeRating = AgeRatingType.TwelvePlus,
            Description = "Woodland wargame",
            Price = 290m
        };

        await _productRepo.AddAsync(product);
        await _productRepo.SaveAsync();

        var fetched = await _productRepo.GetByIdAsync(100, IncludeBehaviour.NoInclude);
        Assert.IsNotNull(fetched);
        Assert.AreEqual("Root", fetched!.Name);

        fetched.Price = 250m;
        _productRepo.Update(fetched);
        await _productRepo.SaveAsync();

        var updated = await _productRepo.GetByIdAsync(100, IncludeBehaviour.NoInclude);
        Assert.AreEqual(250m, updated!.Price);

        _productRepo.Delete(updated);
        await _productRepo.SaveAsync();

        var deleted = await _productRepo.GetByIdAsync(100, IncludeBehaviour.NoInclude);
        Assert.IsNull(deleted);
    }
}
