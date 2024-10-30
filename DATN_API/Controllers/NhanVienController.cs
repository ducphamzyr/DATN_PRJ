using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly IAllRepositories<NhanVien> _nhanVienRepository;

        public NhanVienController(IAllRepositories<NhanVien> nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        // Lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var nhanViens = await _nhanVienRepository.GetAll();
            return Ok(nhanViens);
        }

        // Lấy theo id

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var nhanVien = await _nhanVienRepository.GetById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return Ok(nhanVien);
        }

        // Thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(NhanVien nhanVien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _nhanVienRepository.Create(nhanVien);
                    return Ok(nhanVien);
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
        public async Task<IActionResult> Update(NhanVien nhanVien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _nhanVienRepository.Update(nhanVien);
                    return Ok(nhanVien);
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
            var nhanVien = await _nhanVienRepository.GetById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            await _nhanVienRepository.Delete(nhanVien);
            return Ok();
        }
    }
}
