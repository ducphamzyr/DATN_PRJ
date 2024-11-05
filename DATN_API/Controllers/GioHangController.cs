using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly IAllRepositories<GioHang> _repoGioHang;
        private readonly IAllRepositories<GioHangChiTiet> _repoGioHangChiTiet;

        public GioHangController(IAllRepositories<GioHang> repoGioHang, IAllRepositories<GioHangChiTiet> repoGioHangChiTiet)
        {
            _repoGioHang = repoGioHang;
            _repoGioHangChiTiet = repoGioHangChiTiet;
        }

        // GET: api/GioHang/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGioHang(string id)
        {
            var gioHang = await _repoGioHang.GetById(id);
            if (gioHang == null)
            {
                return NotFound("Giỏ hàng không tồn tại.");
            }

            return Ok(gioHang);
        }
        [HttpGet("ChiTiet/{id}")]
        public async Task<IActionResult> GetGioHangChiTiet(string id)
        {
            var chiTietGioHang = (await _repoGioHangChiTiet.GetAll()).Where(x => x.TenNguoiDung == id).ToList();
            if (!chiTietGioHang.Any())
            {
                return NotFound("Không có chi tiết giỏ hàng nào cho người dùng này.");
            }

            return Ok(chiTietGioHang);
        }
    }
}
