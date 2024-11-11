using DATN_MVC.Models;

namespace DATN_MVC.Services
{
    public interface IPhanLoaiService : IBaseService<PhanLoaiDTO, CreatePhanLoaiDTO, UpdatePhanLoaiDTO>
    {
        // Thêm các phương thức đặc biệt nếu cần
    }

    public class PhanLoaiService : BaseService<PhanLoaiDTO, CreatePhanLoaiDTO, UpdatePhanLoaiDTO>, IPhanLoaiService
    {
        public PhanLoaiService(HttpClient httpClient, IConfiguration config)
            : base(httpClient, config, "phanloai")
        {
        }
    }
}
