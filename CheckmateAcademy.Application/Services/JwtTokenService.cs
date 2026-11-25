using CheckmateAcademy.Application.Interfaces.Services;
using CheckmateAcademy.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CheckmateAcademy.Application.Services
{
    public sealed class JwtTokenService : ITokenService
    {
        private readonly JwtTokenOptions _options;

        public JwtTokenService(JwtTokenOptions options)
        {
            _options = options
                ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(_options.Issuer))
                throw new ArgumentException("JWT issuer is required.");

            if (string.IsNullOrWhiteSpace(_options.Audience))
                throw new ArgumentException("JWT audience is required.");

            if (Encoding.UTF8.GetByteCount(_options.SecretKey) < 32)
                throw new ArgumentException("JWT secret key must contain at least 32 bytes.");

            if (_options.ExpirationMinutes <= 0)
                throw new ArgumentException("JWT expiration must be greater than zero.");
        }

        public string GenerateToken(int userId, Role role)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.SecretKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
