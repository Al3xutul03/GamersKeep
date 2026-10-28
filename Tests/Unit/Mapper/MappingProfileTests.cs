using AutoMapper;
using BusinessLogic.Mapper;
using BusinessLogic.Models.Product;
using BusinessLogic.Models.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Entity;
using Repository.Enums.Types;

namespace Tests.Unit.Mapper;

[TestClass]
[TestCategory("Unit")]
public class MappingProfileTests
{
    private IMapper _mapper = null!;

    [TestInitialize]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));
        var provider = services.BuildServiceProvider();
        _mapper = provider.GetRequiredService<IMapper>();
    }

    [TestMethod]
    public void Map_UserCreateDtoToUser_MapsCorrectly()
    {
        var dto = new UserCreateModel("John Doe", "john@test.com", "secret");
        
        var entity = _mapper.Map<User>(dto);
        
        Assert.AreEqual("John Doe", entity.Name);
        Assert.AreEqual("john@test.com", entity.Email);
        Assert.IsNotNull(entity.Settings);
    }

    [TestMethod]
    public void Map_UserToUserReadDto_MapsCorrectly()
    {
        var entity = new User
        {
            Id = 1,
            Name = "Jane Doe",
            Email = "jane@test.com",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsAdmin = true,
            Settings = "{}"
        };
        
        var dto = _mapper.Map<UserReadModel>(entity);
        
        Assert.AreEqual(1ul, dto.Id);
        Assert.AreEqual("Jane Doe", dto.Name);
        Assert.AreEqual("jane@test.com", dto.Email);
        Assert.IsTrue(dto.IsAdmin);
        Assert.IsNotNull(dto.Settings);
    }

    [TestMethod]
    public void Map_UserUpdateDtoToUser_MapsCorrectly()
    {
        var settings = new BusinessLogic.Json.Model.UserSettings();
        var dto = new UserUpdateModel(1, "Updated Name", "updated@test.com", null, settings);
        
        var entity = _mapper.Map<User>(dto);
        
        Assert.AreEqual("Updated Name", entity.Name);
        Assert.AreEqual("updated@test.com", entity.Email);
        Assert.IsNotNull(entity.Settings);
    }

    [TestMethod]
    public void Map_ProductCreateDtoToProduct_MapsCorrectly()
    {
        var dto = new ProductCreateModel(
            "Catan",
            AgeRatingType.SevenPlus,
            "Strategy board game",
            189.99m,
            10u,
            new List<CategoryType> { CategoryType.BoardGame, CategoryType.Strategy },
            new List<string> { "https://example.com/catan.jpg" });

        var entity = _mapper.Map<Product>(dto);

        Assert.AreEqual("Catan", entity.Name);
        Assert.AreEqual(AgeRatingType.SevenPlus, entity.AgeRating);
        Assert.AreEqual("Strategy board game", entity.Description);
        Assert.AreEqual(189.99m, entity.Price);
        Assert.IsNotNull(entity.Categories);
        Assert.AreEqual(2, entity.Categories.Count);
        Assert.IsNotNull(entity.Images);
        Assert.AreEqual(1, entity.Images.Count);
    }

    [TestMethod]
    public void Map_ProductToProductReadDto_MapsCorrectly()
    {
        var entity = new Product
        {
            Id = 10,
            Name = "Gloomhaven",
            AgeRating = AgeRatingType.SixteenPlus,
            Description = "Dungeon crawler",
            Price = 699.00m,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Categories = new List<ProductCategory>
            {
                new ProductCategory { ProductId = 10, CategoryId = CategoryType.Fantasy }
            },
            Images = new List<ProductImage>
            {
                new ProductImage { ProductId = 10, ImageUrl = "https://example.com/gh.jpg" }
            }
        };

        var dto = _mapper.Map<ProductReadModel>(entity);

        Assert.AreEqual(10ul, dto.Id);
        Assert.AreEqual("Gloomhaven", dto.Name);
        Assert.AreEqual(AgeRatingType.SixteenPlus, dto.AgeRating);
        Assert.AreEqual(699.00m, dto.Price);
        Assert.AreEqual(1, dto.Categories.Count);
        Assert.IsTrue(dto.Categories.Contains(CategoryType.Fantasy));
        Assert.AreEqual(1, dto.Images.Count);
        Assert.IsTrue(dto.Images.Contains("https://example.com/gh.jpg"));
    }

    [TestMethod]
    public void Map_ProductToProductListingDto_MapsCorrectly()
    {
        var entity = new Product
        {
            Id = 10,
            Name = "Gloomhaven",
            AgeRating = AgeRatingType.SixteenPlus,
            Description = "Dungeon crawler",
            Price = 699.00m,
            Categories = new List<ProductCategory>
            {
                new ProductCategory { ProductId = 10, CategoryId = CategoryType.Fantasy }
            },
            Images = new List<ProductImage>
            {
                new ProductImage { ProductId = 10, ImageUrl = "https://example.com/gh1.jpg" },
                new ProductImage { ProductId = 10, ImageUrl = "https://example.com/gh2.jpg" }
            }
        };

        var dto = _mapper.Map<ProductListingModel>(entity);

        Assert.AreEqual(10ul, dto.Id);
        Assert.AreEqual("Gloomhaven", dto.Name);
        Assert.AreEqual("Dungeon crawler", dto.ShortDescription);
        Assert.AreEqual("https://example.com/gh1.jpg", dto.FirstImageUrl);
        Assert.IsTrue(dto.Categories.Contains(CategoryType.Fantasy));
    }

    [TestMethod]
    public void Map_ProductUpdateDtoToProduct_MapsCorrectly()
    {
        var dto = new ProductUpdateModel(
            10,
            "Gloomhaven 2nd Edition",
            AgeRatingType.SixteenPlus,
            "Updated description",
            649.00m,
            5u,
            new List<CategoryType> { CategoryType.Cooperative });

        var entity = _mapper.Map<Product>(dto);

        Assert.AreEqual(10ul, entity.Id);
        Assert.AreEqual("Gloomhaven 2nd Edition", entity.Name);
        Assert.AreEqual(649.00m, entity.Price);
        Assert.IsNotNull(entity.Categories);
        Assert.AreEqual(1, entity.Categories.Count);
        Assert.AreEqual(10ul, entity.Categories.First().ProductId);
    }
}
