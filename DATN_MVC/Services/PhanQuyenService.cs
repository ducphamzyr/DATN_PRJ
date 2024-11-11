using System.Text.Json;
using DATN_MVC.Models;

namespace DATN_MVC.Services
{
    public interface IPhanQuyenService : IBaseService<PhanQuyenDTO, CreatePhanQuyenDTO, UpdatePhanQuyenDTO>
    {
        Task<ApiResponse<Dictionary<string, int>>> LayThongKeAsync(string token);
        Task<ApiResponse<PhanQuyenDTO>> LayQuyenCuaToiAsync(string token);
    }

    public class PhanQuyenService : BaseService<PhanQuyenDTO, CreatePhanQuyenDTO, UpdatePhanQuyenDTO>, IPhanQuyenService
    {
        public PhanQuyenService(HttpClient httpClient, IConfiguration config)
            : base(httpClient, config, "phanquyen")
        {
        }

        public async Task<ApiResponse<Dictionary<string, int>>> LayThongKeAsync(string token)
        {
            try
            {
                ConfigureTokenNew(token);
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/statistics");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<Dictionary<string, int>>>(content, _jsonOptions)
                        ?? new ApiResponse<Dictionary<string, int>> { Message = "Không thể chuyển đổi dữ liệu" };
                }

                return new ApiResponse<Dictionary<string, int>> { Message = "Không thể lấy thống kê" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Dictionary<string, int>> { Message = $"Lỗi hệ thống: {ex.Message}" };
            }
        }

        public async Task<ApiResponse<PhanQuyenDTO>> LayQuyenCuaToiAsync(string token)
        {
            try
            {
                ConfigureTokenNew(token);
                var response = await _httpClient.GetAsync($"{_apiEndpoint}/my-role");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<PhanQuyenDTO>>(content, _jsonOptions)
                        ?? new ApiResponse<PhanQuyenDTO> { Message = "Không thể chuyển đổi dữ liệu" };
                }

                return new ApiResponse<PhanQuyenDTO> { Message = "Không thể lấy thông tin quyền" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<PhanQuyenDTO> { Message = $"Lỗi hệ thống: {ex.Message}" };
            }
        }
    }
}