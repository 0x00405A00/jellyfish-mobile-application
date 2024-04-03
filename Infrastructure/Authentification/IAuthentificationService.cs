
using Shared.DataTransferObject;

namespace Infrastructure.Authentification
{
    public interface IAuthentificationService
    {
        Task<bool> Authentificate(string username, string password, CancellationToken cancellationToken);
        Task<bool> Logout(CancellationToken cancellationToken);
        Task<AuthDTO> GetAuthentification(CancellationToken cancellationToken);
    }
}