using Participants.Domain.Repositories;

namespace Participants.Api.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates a participant using their username and password.
        /// </summary>
        /// <param name="username">The username of the participant.</param>
        /// <param name="password">The password of the participant.</param>
        /// <returns>A JWT token if authentication is successful, otherwise an empty string.</returns>
        Task<string> Authenticate(string username, string password);

        /// <summary>
        /// Logs out a participant by invalidating their token.
        /// </summary>
        /// <param name="token">The token to invalidate.</param>
        void Logout(string token);
    }

    public sealed class AuthenticationService(IParticipantRepository participantRepository, ITokenService tokenService) : IAuthenticationService
    {
        private readonly IParticipantRepository _participantRepository = participantRepository;
        private readonly ITokenService _tokenService = tokenService;

        /// <summary>
        /// Authenticates a participant using their username and password.
        /// </summary>
        /// <param name="username">The username of the participant.</param>
        /// <param name="password">The password of the participant.</param>
        /// <returns>A JWT token if authentication is successful, otherwise an empty string.</returns>
        public async Task<string> Authenticate(string username, string password)
        {
            var participant = await _participantRepository.GetByUsernameAsync(username);

            if (participant is null) return "";

            if (password != participant.Password) return "";

            // Generate JWT token
            var token = _tokenService.GenerateToken(participant);

            return token;
        }

        /// <summary>
        /// Logs out a participant by invalidating their token.
        /// </summary>
        /// <param name="token">The token to invalidate.</param>
        public void Logout(string token)
        {
            _tokenService.InvalidateToken(token);
        }
    }
}
