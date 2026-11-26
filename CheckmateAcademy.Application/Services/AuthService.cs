
using CheckmateAcademy.Application.Configuration;
using CheckmateAcademy.Application.DTOs.Requests.Auth;
using CheckmateAcademy.Application.DTOs.Responses.Auth;
using CheckmateAcademy.Application.Interfaces.Repositories;
using CheckmateAcademy.Application.Interfaces.Services;
using CheckmateAcademy.Domain.Enums;
using CheckmateAcademy.Domain.Factories;
using CheckmateAcademy.Domain.ValueObjects.Profile;
using Microsoft.Extensions.Options;

namespace CheckmateAcademy.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICurrentUserService _currentUserService;
        private readonly AuthenticationOptions _authenticationOptions;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ICurrentUserService currentUserService,
            IOptions<AuthenticationOptions> authenticationOptions)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _currentUserService = currentUserService;
            _authenticationOptions = authenticationOptions.Value;
        }

        public async Task<AuthResponse> RegisterAsync(
            RegisterRequest request,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var email = new Email(request.Email);

            var existingUser = await _userRepository.GetUserByEmailAsync(
                email,
                ct);

            if (existingUser is not null)
                throw new InvalidOperationException(
                    "A user with this email already exists.");

            var role = ResolveRole(request.SecretKey);

            var userId = await _userRepository.GetNextIdAsync(ct);
            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = UserFactory.CreateUser(
                userId,
                request.Name,
                request.Surname,
                request.BirthDate,
                request.Email,
                passwordHash,
                role);

            await _userRepository.CreateUserAsync(user, ct);

            _currentUserService.SetCurrentUser(user.Id, user.Account.Role);

            return new AuthResponse(user.Id, user.Account.Role);
        }

        public async Task<AuthResponse> LoginAsync(
            LoginRequest request,
            CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var email = new Email(request.Email);

            var user = await _userRepository.GetUserByEmailAsync(
                email,
                ct);

            if (user is null ||
                !_passwordHasher.Verify(
                    request.Password,
                    user.Account.PasswordHash))
            {
                throw new UnauthorizedAccessException(
                    "Invalid email or password.");
            }

            _currentUserService.SetCurrentUser(user.Id, user.Account.Role);

            return new AuthResponse(user.Id, user.Account.Role);
        }

        public void Logout()
        {
            _currentUserService.Clear();
        }

        private Role ResolveRole(string? secretKey)
        {
            if (string.IsNullOrWhiteSpace(secretKey))
                return Role.Customer;

            if (string.IsNullOrWhiteSpace(
                    _authenticationOptions.AdminSecretKey) ||
                !string.Equals(
                    secretKey,
                    _authenticationOptions.AdminSecretKey,
                    StringComparison.Ordinal))
            {
                throw new UnauthorizedAccessException(
                    "Invalid administrator secret key.");
            }

            return Role.Admin;
        }
    }
}
