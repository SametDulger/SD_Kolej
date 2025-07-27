using System.Text;
using System.Text.Json;

namespace SDKolej.WebUI.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiService> _logger;

        public ApiService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            var apiBaseUrl = _configuration["ApiBaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                throw new InvalidOperationException("ApiBaseUrl configuration is missing");
            }
            
            _httpClient.BaseAddress = new Uri(apiBaseUrl);
            _logger.LogInformation("ApiService initialized with base URL: {BaseUrl}", apiBaseUrl);
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                _logger.LogDebug("Making GET request to: {Endpoint}", endpoint);
                
                var response = await _httpClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                _logger.LogDebug("GET request successful for: {Endpoint}", endpoint);
                return result ?? throw new InvalidOperationException("Deserialized result is null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET request failed for endpoint: {Endpoint}", endpoint);
                throw new Exception($"API GET isteği başarısız: {ex.Message}", ex);
            }
        }

        public async Task<T> GetAsync<T>(string endpoint, string token)
        {
            try
            {
                _logger.LogDebug("Making authenticated GET request to: {Endpoint}", endpoint);
                
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                _logger.LogDebug("Authenticated GET request successful for: {Endpoint}", endpoint);
                return result ?? throw new InvalidOperationException("Deserialized result is null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Authenticated GET request failed for endpoint: {Endpoint}", endpoint);
                throw new Exception($"Authenticated API GET isteği başarısız: {ex.Message}", ex);
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                _logger.LogDebug("Making POST request to: {Endpoint}", endpoint);
                
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                _logger.LogDebug("POST request successful for: {Endpoint}", endpoint);
                return result ?? throw new InvalidOperationException("Deserialized result is null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST request failed for endpoint: {Endpoint}", endpoint);
                throw new Exception($"API POST isteği başarısız: {ex.Message}", ex);
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data, string token)
        {
            try
            {
                _logger.LogDebug("Making authenticated POST request to: {Endpoint}", endpoint);
                
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
                {
                    Content = content
                };
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                _logger.LogDebug("Authenticated POST request successful for: {Endpoint}", endpoint);
                return result ?? throw new InvalidOperationException("Deserialized result is null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Authenticated POST request failed for endpoint: {Endpoint}", endpoint);
                throw new Exception($"Authenticated API POST isteği başarısız: {ex.Message}", ex);
            }
        }

        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                _logger.LogDebug("Making PUT request to: {Endpoint}", endpoint);
                
                var json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync(endpoint, content);
                response.EnsureSuccessStatusCode();
                
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                _logger.LogDebug("PUT request successful for: {Endpoint}", endpoint);
                return result ?? throw new InvalidOperationException("Deserialized result is null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT request failed for endpoint: {Endpoint}", endpoint);
                throw new Exception($"API PUT isteği başarısız: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                _logger.LogDebug("Making DELETE request to: {Endpoint}", endpoint);
                
                var response = await _httpClient.DeleteAsync(endpoint);
                var success = response.IsSuccessStatusCode;
                
                _logger.LogDebug("DELETE request {Result} for: {Endpoint}", 
                    success ? "successful" : "failed", endpoint);
                
                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE request failed for endpoint: {Endpoint}", endpoint);
                throw new Exception($"API DELETE isteği başarısız: {ex.Message}", ex);
            }
        }
    }
} 