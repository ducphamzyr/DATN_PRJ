using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly IAllRepositories<GioHang> _gioHangRepository;

        public GioHangController(IAllRepositories<GioHang> gioHangRepository)
        {
            _gioHangRepository = gioHangRepository;
        }

        // Lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var gioHangs = await _gioHangRepository.GetAll();
            return Ok(gioHangs);
        }

        // Lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var gioHang = await _gioHangRepository.GetById(id);
            if (gioHang == null)
            {
                return NotFound();
            }
            return Ok(gioHang);
        }

        // Thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(GioHang gioHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _gioHangRepository.Create(gioHang);
                    return Ok(gioHang);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return BadRequest();
            }
        }

        // Sửa

        [HttpPut("update")]
        public async Task<IActionResult> Update(GioHang gioHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _gioHangRepository.Update(gioHang);
                    return Ok(gioHang);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return BadRequest();
            }
        }

        // Xóa
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var gioHang = await _gioHangRepository.GetById(id);
            if (gioHang == null)
            {
                return NotFound();
            }
            await _gioHangRepository.Delete(gioHang);
            return Ok();
        }
    }
}
