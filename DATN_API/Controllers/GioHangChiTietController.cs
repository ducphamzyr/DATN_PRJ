using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangChiTietController : ControllerBase
    {
        private readonly IAllRepositories<GioHangChiTiet> _repoGioHangChiTiet;

        public GioHangChiTietController(IAllRepositories<GioHangChiTiet> repoGioHangChiTiet)
        {
            _repoGioHangChiTiet = repoGioHangChiTiet;
        }

        // Lấy tất cả chi tiết giỏ hàng
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var gioHangChiTiets = await _repoGioHangChiTiet.GetAll();
            return Ok(gioHangChiTiets);
        }

        // Lấy chi tiết giỏ hàng theo ID
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var gioHangChiTiet = await _repoGioHangChiTiet.GetById(id);
            if (gioHangChiTiet == null)
            {
                return NotFound();
            }
            return Ok(gioHangChiTiet);
        }

        // Thêm mới chi tiết giỏ hàng
        [HttpPost("create")]
        public async Task<IActionResult> Create(GioHangChiTiet gioHangChiTiet)
        {
            if (ModelState.IsValid)
            {
                await _repoGioHangChiTiet.Create(gioHangChiTiet);
                return Ok(gioHangChiTiet);
            }
            return BadRequest();
        }

        // Cập nhật chi tiết giỏ hàng
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, GioHangChiTiet gioHangChiTiet)
        {
            if (id != gioHangChiTiet.GioHangChiTietID)
            {
                return BadRequest();
            }

            var existingGioHangChiTiet = await _repoGioHangChiTiet.GetById(id);
            if (existingGioHangChiTiet == null)
            {
                return NotFound();
            }

            existingGioHangChiTiet.SanPhamID = gioHangChiTiet.SanPhamID;
            existingGioHangChiTiet.SoLuong = gioHangChiTiet.SoLuong;

            await _repoGioHangChiTiet.Update(existingGioHangChiTiet);
            return NoContent();
        }

        // Xóa chi tiết giỏ hàng
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var gioHangChiTiet = await _repoGioHangChiTiet.GetById(id);
            if (gioHangChiTiet == null)
            {
                return NotFound();
            }

            await _repoGioHangChiTiet.Delete(id);
            return NoContent();
        }
    }
}
