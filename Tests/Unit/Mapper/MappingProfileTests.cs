using AutoMapper;
using BusinessLogic.Mapper;
using BusinessLogic.Models.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Entity;

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
}
