using Microsoft.IdentityModel.Tokens;
using Participants.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Participants.Api.Services
{
    public interface ITokenService
    {
        string GenerateToken(Participant user);
        void InvalidateToken(string token);
    }

    public sealed class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(Participant user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("ParticipantId", user.Id.ToString())
            };

            // Create the security key and signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration time
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void InvalidateToken(string token)
        {
            // Token invalidation logic (if using a token store)
            // For JWT, typically tokens are just not renewed or stored.
            throw new NotImplementedException();
        }
    }
}
