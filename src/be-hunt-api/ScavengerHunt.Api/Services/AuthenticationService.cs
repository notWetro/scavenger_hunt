using ScavengerHunt.Domain.Repositories;

namespace ScavengerHunt.Api.Services
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(string username, string password);
        void Logout(string token);
    }

    public class AuthenticationService(IParticipantRepository participantRepository, ITokenService tokenService) : IAuthenticationService
    {
        private readonly IParticipantRepository _participantRepository = participantRepository;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<string> Authenticate(string username, string password)
        {
            var participant = await _participantRepository.GetByUsernameAsync(username);

            if (participant is null) return "";

            if (password != participant.Password) return "";

            // Generate JWT token
            var token = _tokenService.GenerateToken(participant);

            // TODO: Store Login-Token

            return token;
        }

        public void Logout(string token)
        {
            _tokenService.InvalidateToken(token);
        }
    }
}
