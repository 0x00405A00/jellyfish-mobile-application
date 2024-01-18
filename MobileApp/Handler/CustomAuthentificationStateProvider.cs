using Shared.DataTransferObject;
using Shared.Infrastructure.Backend;
using Shared.Infrastructure.Backend.Api;

namespace Infrastructure.Handler
{
    public class CustomAuthentificationStateProvider : ICustomAuthentificationStateProvider
    {
        public Task<AuthDTO> GetCurrentAuthentification(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Login(string userName, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logout(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AuthDTO> RefreshLogin(string token, string refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerUserDTO, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
