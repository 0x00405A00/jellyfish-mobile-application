using Microsoft.Maui.Storage;
using Shared.DataTransferObject;
using Shared.Infrastructure.Backend;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;

namespace Infrastructure.Storage
{
    public class LocalStorageService : ILocalStorageService
    {
        public async Task SetItem(string key, string value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("key is null");
            }
            await SecureStorage.Default.SetAsync(key, value);
        }

        public async Task<string> GetItem(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("key is null");
            }
            try
            {
                return await SecureStorage.Default.GetAsync(key);
            }
            catch (System.Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

        public async Task RemoveItem(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("key is null");
            }
            try
            {
                SecureStorage.Default.Remove(key);
            }
            catch (System.Exception ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
        }

        public async Task<T> GetDeserializedJsonItemFromKey<T>(string key, [CallerMemberName] object caller = null)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("key is null");
            }
            try
            {

                string json = await GetItem(key);
                if (string.IsNullOrWhiteSpace(json))
                    return default;
                T value = JsonSerializer.Deserialize<T>(json);

                return value;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task SetDeserializedJsonItemFromKey<T>(string key, T value)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("key is null");
            }
            string str = JsonSerializer.Serialize(value, typeof(T));
            await SecureStorage.Default.SetAsync(key, str);
        }

        public async Task<bool> CheckIfValidTokenExists()
        {
            try
            {
                var value = await GetDeserializedJsonItemFromKey<AuthDTO>(Shared.Const.AuthorizationConst.SessionStorage.AuthorizationKey);
                if (value != null)
                {
                    return !value.IsTokenExpired;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
