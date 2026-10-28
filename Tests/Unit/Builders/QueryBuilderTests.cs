using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Builders;
using Repository.Enums.Behaviours;

namespace Tests.Unit.Builders;

public class DummyEntity
{
    public int Id { get; set; }
    public DummyRelatedEntity? Related { get; set; }
}

public class DummyRelatedEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

public class DummyDbContext : DbContext
{
    public DbSet<DummyEntity> Dummies { get; set; } = null!;
    public DbSet<DummyRelatedEntity> RelatedDummies { get; set; } = null!;
    
    public DummyDbContext(DbContextOptions<DummyDbContext> options) : base(options) { }
}

[TestClass]
[TestCategory("Unit")]
public class QueryBuilderTests
{
    private DummyDbContext _context = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DummyDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new DummyDbContext(options);
        
        _context.Dummies.Add(new DummyEntity { Id = 1, Related = new DummyRelatedEntity { Id = 10, Name = "Test" } });
        _context.SaveChanges();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [TestMethod]
    public void Build_NoIncludes_ReturnsQueryWithoutJoins()
    {
        var builder = new QueryBuilder<DummyEntity>(_context.Dummies);
        builder.AddBehavior(IncludeBehaviour.NoInclude);
        
        var query = builder.Build();
        var result = query.First();
        
        // In InMemory DB, navigation properties are populated if the related entity is tracked.
        // We clear the tracker to truly test includes.
        _context.ChangeTracker.Clear();
        var queriedResult = builder.Build().First();
        
        Assert.IsNull(queriedResult.Related);
    }

    [TestMethod]
    public void Build_AllIncludes_ReturnsQueryWithAllJoins()
    {
        var builder = new QueryBuilder<DummyEntity>(_context.Dummies);
        builder.AddBehavior(IncludeBehaviour.AllIncludes);
        
        _context.ChangeTracker.Clear();
        var query = builder.Build();
        var result = query.First();
        
        Assert.IsNotNull(result.Related);
        Assert.AreEqual("Test", result.Related!.Name);
    }

    [TestMethod]
    public void Build_GivenIncludes_ReturnsQueryWithSpecifiedJoins()
    {
        var builder = new QueryBuilder<DummyEntity>(_context.Dummies);
        builder.AddBehavior(IncludeBehaviour.GivenIncludes);
        builder.AddIncludes(q => q.Include(e => e.Related));
        
        _context.ChangeTracker.Clear();
        var query = builder.Build();
        var result = query.First();
        
        Assert.IsNotNull(result.Related);
        Assert.AreEqual("Test", result.Related!.Name);
    }
}
