using DATN_MVC.Models;
using DATN_MVC.Models.PhuongThucThanhToan;
using System.Text.Json;

namespace DATN_MVC.Services
{
    public interface IPhuongThucThanhToanService : IBaseService<PhuongThucThanhToanDTO, CreatePhuongThucThanhToanDTO_MVC, UpdatePhuongThucThanhToanDTO_MVC>
    {
        Task<ApiResponse<Dictionary<string, int>>> LayThongKeAsync(string token);
    }
    public class PhuongThucThanhToanService : BaseService<PhuongThucThanhToanDTO, CreatePhuongThucThanhToanDTO_MVC, UpdatePhuongThucThanhToanDTO_MVC>, IPhuongThucThanhToanService
    {
        public PhuongThucThanhToanService(HttpClient httpClient, IConfiguration config)
            : base(httpClient, config, "PhuongThucThanhToan")
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
    }
}
