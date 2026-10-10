using BusinessLogic.Models.User;
using BusinessLogic.Models.Login;

namespace BusinessLogic.Services.Abstractions
{
    /// <summary>
    /// Authentication service interface, implemented by <see cref="AuthService"/>
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Validates a raw JWT string against the server's security key and validation parameters.
        /// </summary>
        /// <param name="token">The JWT string to verify.</param>
        /// <returns>True if the token is valid and not expired; otherwise, false.</returns>
        Task<bool> IsLoggedIn(string token);

        /// <summary>
        /// Verifies user credentials and issues a new pair of Access and Refresh tokens.
        /// </summary>
        /// <param name="model">The login credentials.</param>
        /// <returns>A <see cref="LoginResponseModel"/> containing tokens, or null if unauthorized.</returns>
        Task<LoginResponseModel?> Login(LoginModel model);

        /// <summary>
        /// Creates a new user record and returns an initial set of authentication tokens.
        /// </summary>
        /// <param name="model">The user details for registration.</param>
        /// <returns>A <see cref="LoginResponseModel"/> if successful; null if the user already exists.</returns>
        Task<LoginResponseModel?> Register(UserCreateModel model);

        /// <summary>
        /// Validates an expired access token and a refresh token to provide a new token pair.
        /// This implements "Refresh Token Rotation" for enhanced security.
        /// </summary>
        /// <param name="oldResponse">The expired Access Token and the current Refresh Token.</param>
        /// <returns>A new <see cref="LoginResponseModel"/>, or null if the refresh token is invalid/expired.</returns>
        Task<LoginResponseModel?> RefreshToken(LoginResponseModel oldResponse);
    }
}
