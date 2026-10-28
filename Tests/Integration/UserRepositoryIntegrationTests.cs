using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories;

namespace Tests.Integration;

[TestClass]
[DoNotParallelize]
[TestCategory("Integration")]
public class UserRepositoryIntegrationTests
{
    [ClassInitialize]
    public static async Task StartContainer(TestContext _) => await MySqlContainerFixture.StartAsync();

    [ClassCleanup]
    public static async Task StopContainer() => await MySqlContainerFixture.StopAsync();

    [TestInitialize]
    public async Task ResetDatabase() => await MySqlContainerFixture.ResetAsync();

    [TestMethod]
    public async Task AddAsync_RealData_RoundTripsThroughMySql()
    {
        await using (var writeContext = MySqlContainerFixture.CreateContext())
        {
            var repo = new UserRepository(writeContext);
            await repo.AddAsync(new User { Name = "TestUser", Email = "test@example.com", PasswordHash = "hash", Settings = "{}" });
            await repo.SaveAsync();
        }

        await using var readContext = MySqlContainerFixture.CreateContext();
        var stored = await new UserRepository(readContext).GetByNameAsync("TestUser");

        Assert.IsNotNull(stored);
        Assert.AreEqual("test@example.com", stored!.Email);
    }

    [TestMethod]
    public async Task GetByNameAsync_TranslatesToSql_ReturnsMatch()
    {
        await using var context = MySqlContainerFixture.CreateContext();
        var repo = new UserRepository(context);
        await repo.AddAsync(new User { Name = "Ada", Email = "ada@test.com", PasswordHash = "h1", Settings = "{}" });
        await repo.AddAsync(new User { Name = "Grace", Email = "grace@test.com", PasswordHash = "h2", Settings = "{}" });
        await repo.SaveAsync();

        var result = await repo.GetByNameAsync("Grace");

        Assert.IsNotNull(result);
        Assert.AreEqual("grace@test.com", result!.Email);
    }

    [TestMethod]
    public async Task EmailExistsAsync_ReturnsTrueIfMatch()
    {
        await using var context = MySqlContainerFixture.CreateContext();
        var repo = new UserRepository(context);
        await repo.AddAsync(new User { Name = "Ada", Email = "ada@test.com", PasswordHash = "h1", Settings = "{}" });
        await repo.SaveAsync();

        var exists = await repo.EmailExistsAsync("ada@test.com");
        var doesNotExist = await repo.EmailExistsAsync("nobody@test.com");

        Assert.IsTrue(exists);
        Assert.IsFalse(doesNotExist);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsCorrectEntity()
    {
        await using var context = MySqlContainerFixture.CreateContext();
        var repo = new UserRepository(context);
        var user = new User { Name = "Turing", Email = "alan@test.com", PasswordHash = "h3", Settings = "{}" };
        await repo.AddAsync(user);
        await repo.SaveAsync();

        var result = await repo.GetByIdAsync(user.Id, IncludeBehaviour.NoInclude);

        Assert.IsNotNull(result);
        Assert.AreEqual("Turing", result!.Name);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        await using var context = MySqlContainerFixture.CreateContext();
        var repo = new UserRepository(context);
        await repo.AddAsync(new User { Name = "U1", Email = "u1@test.com", PasswordHash = "h1", Settings = "{}" });
        await repo.AddAsync(new User { Name = "U2", Email = "u2@test.com", PasswordHash = "h2", Settings = "{}" });
        await repo.SaveAsync();

        var result = await repo.GetAllAsync(IncludeBehaviour.NoInclude);

        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public async Task Update_ModifiesEntity()
    {
        await using var context = MySqlContainerFixture.CreateContext();
        var repo = new UserRepository(context);
        var user = new User { Name = "U1", Email = "u1@test.com", PasswordHash = "h1", Settings = "{}" };
        await repo.AddAsync(user);
        await repo.SaveAsync();

        user.Name = "Updated";
        repo.Update(user);
        await repo.SaveAsync();

        var result = await repo.GetByIdAsync(user.Id, IncludeBehaviour.NoInclude);
        Assert.AreEqual("Updated", result!.Name);
    }

    [TestMethod]
    public async Task Delete_RemovesEntity()
    {
        await using var context = MySqlContainerFixture.CreateContext();
        var repo = new UserRepository(context);
        var user = new User { Name = "U1", Email = "u1@test.com", PasswordHash = "h1", Settings = "{}" };
        await repo.AddAsync(user);
        await repo.SaveAsync();

        repo.Delete(user);
        await repo.SaveAsync();

        var result = await repo.GetByIdAsync(user.Id, IncludeBehaviour.NoInclude);
        Assert.IsNull(result);
    }
}
