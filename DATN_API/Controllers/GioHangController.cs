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

        public GioHangController(IAllRepositories<GioHang> repoGioHang)
        {
            _repoGioHang = repoGioHang;
        }

        // Lấy tất cả giỏ hàng
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var gioHangs = await _repoGioHang.GetAll();
            return Ok(gioHangs);
        }

        // Lấy giỏ hàng theo ID
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var gioHang = await _repoGioHang.GetById(id);
            if (gioHang == null)
            {
                return NotFound();
            }
            return Ok(gioHang);
        }

        // Thêm mới giỏ hàng
        [HttpPost("create")]
        public async Task<IActionResult> Create(GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                await _repoGioHang.Create(gioHang);
                return Ok(gioHang);
            }
            return BadRequest();
        }

        // Cập nhật giỏ hàng
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, GioHang gioHang)
        {
            if (id != gioHang.GioHangID)
            {
                return BadRequest();
            }

            var existingGioHang = await _repoGioHang.GetById(id);
            if (existingGioHang == null)
            {
                return NotFound();
            }

            existingGioHang.TenNguoiDung = gioHang.TenNguoiDung;
            existingGioHang.CapNhatLanCuoi = gioHang.CapNhatLanCuoi;

            await _repoGioHang.Update(existingGioHang);
            return NoContent();
        }

        // Xóa giỏ hàng
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var gioHang = await _repoGioHang.GetById(id);
            if (gioHang == null)
            {
                return NotFound();
            }

            await _repoGioHang.Delete(id);
            return NoContent();
        }
    }
}
