using Shared.DataTransferObject;
using Shared.Infrastructure.Backend;
using Shared.Infrastructure.Backend.Api;

namespace MobileApp.Handler
{
    public class LocalStorageService : ILocalStorageService
    {
        public Task<bool> CheckIfValidTokenExists()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetDeserializedJsonItemFromKey<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetItem(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItem(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetDeserializedJsonItemFromKey<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task SetItem(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
