using AutoMapper;
using BusinessLogic.Mapper;
using BusinessLogic.Models.Address;
using BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Context;
using Repository.Repositories;

namespace Tests.Unit.Services;

[TestClass]
[TestCategory("Unit")]
public class AddressServiceTests
{
    private AppDbContext _context = null!;
    private IMapper _mapper = null!;
    private AddressRepository _addressRepo = null!;
    private AddressService _addressService = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _addressRepo = new AddressRepository(_context);

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));
        var provider = services.BuildServiceProvider();
        _mapper = provider.GetRequiredService<IMapper>();

        _addressService = new AddressService(_mapper, _addressRepo);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    public async Task CreateAddressAsync_FirstAddress_IsAutomaticallyMarkedAsDefault()
    {
        var model = new AddressCreateModel("Acasă", "Andrei Popa", "0722111222", "Str. Florilor 5", "Iași", "Iași", "700100");

        var created = await _addressService.CreateAddressAsync(1, model);

        Assert.IsNotNull(created);
        Assert.IsTrue(created.IsDefault);

        var list = await _addressService.GetUserAddressesAsync(1);
        Assert.HasCount(1, list);
        Assert.IsTrue(list[0].IsDefault);
    }

    [TestMethod]
    public async Task CreateAddressAsync_SecondDefaultAddress_UnsetsFirstDefault()
    {
        var addr1 = await _addressService.CreateAddressAsync(1, new AddressCreateModel("Acasă", "Andrei Popa", "0722111222", "Str. Florilor 5", "Iași", "Iași", "700100"));
        var addr2 = await _addressService.CreateAddressAsync(1, new AddressCreateModel("Birou", "Andrei Popa", "0722111222", "Str. Palat 1", "Iași", "Iași", "700200", IsDefault: true));

        var list = await _addressService.GetUserAddressesAsync(1);
        Assert.HasCount(2, list);

        // Default address is first
        Assert.AreEqual(addr2.Id, list[0].Id);
        Assert.IsTrue(list[0].IsDefault);

        var previous = list.First(a => a.Id == addr1.Id);
        Assert.IsFalse(previous.IsDefault);
    }

    [TestMethod]
    public async Task DeleteAddressAsync_SoftDeletesAddress()
    {
        var created = await _addressService.CreateAddressAsync(1, new AddressCreateModel("Acasă", "Andrei Popa", "0722111222", "Str. Florilor 5", "Iași", "Iași", "700100"));

        var deleted = await _addressService.DeleteAddressAsync(1, created.Id);
        Assert.IsTrue(deleted);

        var list = await _addressService.GetUserAddressesAsync(1);
        Assert.HasCount(0, list);
    }

    [TestMethod]
    public async Task GetUserAddressByIdAsync_EnforcesUserOwnership()
    {
        var user1Addr = await _addressService.CreateAddressAsync(1, new AddressCreateModel("Acasă", "User 1", "0700000001", "Str. 1", "Iași", "Iași", "700100"));

        var retrievedByUser2 = await _addressService.GetUserAddressByIdAsync(2, user1Addr.Id);
        var retrievedByUser1 = await _addressService.GetUserAddressByIdAsync(1, user1Addr.Id);

        Assert.IsNull(retrievedByUser2);
        Assert.IsNotNull(retrievedByUser1);
    }
}
