using BusinessLogic.Models.Login;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Repository.Entity;
using Repository.Repositories.Abstractions;

namespace Tests.Unit.Services;

[TestClass]
[TestCategory("Unit")]
public class AuthServiceTests
{
    private IUserRepository _userRepo = null!;
    private IConfiguration _config = null!;
    private AuthService _authService = null!;

    [TestInitialize]
    public void Setup()
    {
        _userRepo = Substitute.For<IUserRepository>();
        _config = Substitute.For<IConfiguration>();

        _config["Jwt:Key"].Returns("VerySecretKeyThatIsAtLeast32BytesLongForSha256!@#");
        _config["Jwt:Issuer"].Returns("TestIssuer");
        _config["Jwt:Audience"].Returns("TestAudience");
        _config["Jwt:ExpirationTimeMinutes"].Returns("60");

        _authService = new AuthService(_userRepo, _config);
    }

    [TestMethod]
    public async Task Login_InvalidUsername_ReturnsNull()
    {
        _userRepo.GetByNameAsync("MissingUser").Returns((User?)null);
        var result = await _authService.Login(new LoginModel("MissingUser", "password123"));
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Login_InvalidPassword_ReturnsNull()
    {
        var hash = BCrypt.Net.BCrypt.HashPassword("correctpassword");
        _userRepo.GetByNameAsync("RealUser").Returns(new User { Name = "RealUser", PasswordHash = hash });
        
        var result = await _authService.Login(new LoginModel("RealUser", "wrongpassword"));
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Login_ValidCredentials_ReturnsTokens()
    {
        var hash = BCrypt.Net.BCrypt.HashPassword("correctpassword");
        _userRepo.GetByNameAsync("RealUser").Returns(new User { Id = 1, Name = "RealUser", Email = "test@test.com", PasswordHash = hash });
        
        var result = await _authService.Login(new LoginModel("RealUser", "correctpassword"));
        
        Assert.IsNotNull(result);
        Assert.IsFalse(string.IsNullOrWhiteSpace(result!.Token));
        Assert.IsFalse(string.IsNullOrWhiteSpace(result.RefreshToken));
    }

    [TestMethod]
    public async Task Register_ExistingEmail_ReturnsNull()
    {
        _userRepo.EmailExistsAsync("taken@test.com").Returns(true);
        var result = await _authService.Register(new UserCreateModel("NewUser", "taken@test.com", "pass"));
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Register_NewEmail_ReturnsTokensAndAddsUser()
    {
        _userRepo.EmailExistsAsync("new@test.com").Returns(false);
        
        var result = await _authService.Register(new UserCreateModel("NewUser", "new@test.com", "pass"));
        
        Assert.IsNotNull(result);
        await _userRepo.Received(1).AddAsync(Arg.Is<User>(u => u.Name == "NewUser" && u.Email == "new@test.com"));
        await _userRepo.Received(2).SaveAsync();
    }
}
