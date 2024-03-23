using Domain.ValueObjects;
using Infrastructure.Api;
using MediatR;
using Shared.DataTransferObject;
using Shared.Infrastructure.Backend;
using Shared.Infrastructure.Backend.Api;

namespace Infrastructure.Authentification
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IJellyfishBackendApi jellyfishBackendApi;
        private readonly ILocalStorageService localStorageService;

        public AuthentificationService(
            IJellyfishBackendApi jellyfishBackendApi,
            ILocalStorageService localStorageService)
        {
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

            await localStorageService.SetDeserializedJsonItemFromKey(Shared.Const.AuthorizationConst.SessionStorage.AuthorizationKey, response);
            return true;
        }
    }
}
