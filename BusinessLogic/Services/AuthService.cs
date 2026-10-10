using BusinessLogic.Models.Login;
using BusinessLogic.Models.User;
using BusinessLogic.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Entity;
using Repository.Enums.Behaviours;
using Repository.Repositories.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic.Services
{
    /// <summary>
    /// Implements authentication logic including JWT generation, BCrypt password hashing, 
    /// and refresh token rotation.
    /// </summary>
    public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _config = configuration;

        public async Task<LoginResponseModel?> Login(LoginModel model)
        {
            var user = await _userRepository.GetByNameAsync(model.Username);

            // BCrypt.Verify automatically handles the salt stored within the hash string
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return null;

            return await GenerateLoginResponse(user);
        }

        public async Task<LoginResponseModel?> Register(UserCreateModel model)
        {
            if (await _userRepository.EmailExistsAsync(model.Email)) return null;

            // Generate a secure hash. WorkFactor 12 is a good balance of speed vs security.
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password, workFactor: 12);

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = hashedPassword,
                IsAdmin = false,
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return await GenerateLoginResponse(user);
        }

        public async Task<bool> IsLoggedIn(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<LoginResponseModel?> RefreshToken(LoginResponseModel oldResponse)
        {
            var principal = GetPrincipalFromExpiredToken(oldResponse.Token);
            if (principal == null) return null;

            var userIdString = principal.FindFirst("id")?.Value;
            if (!ulong.TryParse(userIdString, out ulong userId)) return null;

            var user = await _userRepository.GetByIdAsync(userId, IncludeBehaviour.NoInclude);

            if (user == null || user.RefreshToken != oldResponse.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null;

            return await GenerateLoginResponse(user);
        }

        private async Task<LoginResponseModel> GenerateLoginResponse(User user)
        {
            var accessToken = CreateToken(user);
            var refreshToken = CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _userRepository.Update(user);
            await _userRepository.SaveAsync();

            return new LoginResponseModel(accessToken, refreshToken);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new ("id", user.Id.ToString()),
                new ("name", user.Name),
                new ("email", user.Email),
                new ("isAdmin", user.IsAdmin.ToString().ToLower())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpirationTimeMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                return null;

            return principal;
        }
    }
}