using System.Text.Json;

namespace SDKolej.WebUI.Services
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string endpoint);
        Task<T> PostAsync<T>(string endpoint, object data);
        Task<T> PutAsync<T>(string endpoint, object data);
        Task<bool> DeleteAsync(string endpoint);
        Task<T> PostAsync<T>(string endpoint, object data, string token);
        Task<T> GetAsync<T>(string endpoint, string token);
    }
} 