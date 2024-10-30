using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangChiTietController : ControllerBase
    {
        private readonly IAllRepositories<GioHangChiTiet> _gioHangChiTietRepository;

        public GioHangChiTietController(IAllRepositories<GioHangChiTiet> gioHangChiTietRepository)
        {
            _gioHangChiTietRepository = gioHangChiTietRepository;
        }

        // Lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var gioHangChiTiets = await _gioHangChiTietRepository.GetAll();
            return Ok(gioHangChiTiets);
        }

        // Lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var gioHangChiTiet = await _gioHangChiTietRepository.GetById(id);
            if (gioHangChiTiet == null)
            {
                return NotFound();
            }
            return Ok(gioHangChiTiet);
        }

        // Thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(GioHangChiTiet gioHangChiTiet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _gioHangChiTietRepository.Create(gioHangChiTiet);
                    return Ok(gioHangChiTiet);
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
        public async Task<IActionResult> Update(GioHangChiTiet gioHangChiTiet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _gioHangChiTietRepository.Update(gioHangChiTiet);
                    return Ok(gioHangChiTiet);
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
            var gioHangChiTiet = await _gioHangChiTietRepository.GetById(id);
            if (gioHangChiTiet == null)
            {
                return NotFound();
            }
            await _gioHangChiTietRepository.Delete(gioHangChiTiet);
            return Ok();
        }

    }
}
