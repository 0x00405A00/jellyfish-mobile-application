using Domain.ValueObjects;
using Infrastructure.Api;
using MediatR;
using Shared.DataTransferObject;
using Shared.Infrastructure.Backend;
using Shared.Infrastructure.Backend.Api;
using Shared.Infrastructure.Backend.SignalR;
using Shared.Infrastructure.Backend.SignalR.Abstraction;

namespace Infrastructure.Authentification
{
    public class AuthentificationService : IAuthentificationService
    {
        public static readonly string AuthorizationKey = Shared.Const.AuthorizationConst.SessionStorage.AuthorizationKey;
        private readonly JellyfishSignalRClient jellyfishSignalRClient;
        private readonly IJellyfishBackendApi jellyfishBackendApi;
        private readonly ILocalStorageService localStorageService;

        public AuthentificationService(
            JellyfishSignalRClient jellyfishSignalRClient,
            IJellyfishBackendApi jellyfishBackendApi,
            ILocalStorageService localStorageService)
        {
            this.jellyfishSignalRClient = jellyfishSignalRClient;
            this.jellyfishBackendApi = jellyfishBackendApi;
            this.localStorageService = localStorageService;
        }

        public async Task<bool> Authentificate(string username, string password, CancellationToken cancellationToken)
        { 
            var response = await jellyfishBackendApi.Authentificate(username, password, cancellationToken);
            if (response == null)
            {
                //login failed
                return false;
            }

            jellyfishSignalRClient.OpenConnection(cancellationToken);
            await localStorageService.SetDeserializedJsonItemFromKey(AuthorizationKey, response);
            return true;
        }

        public Task<AuthDTO> GetAuthentification(CancellationToken cancellationToken)
        {
            return localStorageService.GetDeserializedJsonItemFromKey<AuthDTO>(AuthorizationKey);
        }

        public async Task<bool> Logout(CancellationToken cancellationToken)
        {
            var response = await jellyfishBackendApi.Logout(cancellationToken);

            jellyfishSignalRClient.CloseConnection(cancellationToken);
            await localStorageService.RemoveItem(AuthorizationKey);
            return true;
        }
    }
}
