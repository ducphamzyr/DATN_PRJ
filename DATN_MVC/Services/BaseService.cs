using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using DATN_MVC.Models;

namespace DATN_MVC.Services
{
    public interface IBaseService<T, TCreate, TUpdate>
    {
        Task<ApiResponse<List<T>>> GetAllAsync(string token);
        Task<ApiResponse<T>> GetByIdAsync(int id, string token);
        Task<ApiResponse<T>> CreateAsync(TCreate model, string token);
        Task<ApiResponse<bool>> EditAsync(int id, TUpdate model, string token);
        Task<ApiResponse<bool>> DeleteAsync(int id, string token);
    }

    public abstract class BaseService<T, TCreate, TUpdate> : IBaseService<T, TCreate, TUpdate>
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _apiEndpoint;
        protected readonly JsonSerializerOptions _jsonOptions;

        protected BaseService(HttpClient httpClient, IConfiguration config, string endpoint)
        {
            _httpClient = httpClient;
            var baseUrl = config["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new InvalidOperationException("API Base URL is not configured");

            _httpClient.BaseAddress = new Uri(baseUrl);

            _apiEndpoint = $"api/{endpoint}";

            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        protected void ConfigureTokenNew(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public virtual async Task<ApiResponse<List<T>>> GetAllAsync(string token)
        {
            try
            {
                ConfigureTokenNew(token);
                var response = await _httpClient.GetAsync(_apiEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<List<T>>>(content, _jsonOptions)
                        ?? new ApiResponse<List<T>> { Message = "Không thể chuyển đổi dữ liệu" };
                }

                return new ApiResponse<List<T>> { Message = $"Lỗi: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<T>> { Message = $"Lỗi hệ thống: {ex.Message}" };
            }
        }

        public virtual async Task<ApiResponse<T>> GetByIdAsync(int id, string token)
        {
            try
            {
                ConfigureTokenNew(token);
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<T>>(content, _jsonOptions)
                        ?? new ApiResponse<T> { Message = "Không thể chuyển đổi dữ liệu" };
                }

                return new ApiResponse<T> { Message = $"Lỗi: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Message = $"Lỗi hệ thống: {ex.Message}" };
            }
        }

        public virtual async Task<ApiResponse<T>> CreateAsync(TCreate model, string token)
        {
            try
            {
                ConfigureTokenNew(token);
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_apiEndpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<T>>(responseContent, _jsonOptions)
                        ?? new ApiResponse<T> { Message = "Không thể chuyển đổi dữ liệu" };
                }

                return new ApiResponse<T> { Message = $"Lỗi: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Message = $"Lỗi hệ thống: {ex.Message}" };
            }
        }

        public virtual async Task<ApiResponse<bool>> EditAsync(int id, TUpdate model, string token)
        {
            try
            {
                ConfigureTokenNew(token);
                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_apiEndpoint}/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<bool>>(responseContent, _jsonOptions)
                        ?? new ApiResponse<bool> { Message = "Không thể chuyển đổi dữ liệu" };
                }

                return new ApiResponse<bool> { Message = $"Lỗi: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Message = $"Lỗi hệ thống: {ex.Message}" };
            }
        }

        public virtual async Task<ApiResponse<bool>> DeleteAsync(int id, string token)
        {
            try
            {
                ConfigureTokenNew(token);
                var response = await _httpClient.DeleteAsync($"{_apiEndpoint}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<bool>>(responseContent, _jsonOptions)
                        ?? new ApiResponse<bool> { Message = "Không thể chuyển đổi dữ liệu" };
                }

                return new ApiResponse<bool> { Message = $"Lỗi: {response.StatusCode}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Message = $"Lỗi hệ thống: {ex.Message}" };
            }
        }
    }
}