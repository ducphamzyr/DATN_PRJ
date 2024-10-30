using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController : ControllerBase
    {
        private readonly IAllRepositories<DonHang> _donHangRepository;

        public DonHangController(IAllRepositories<DonHang> donHangRepository)
        {
            _donHangRepository = donHangRepository;
        }

        // Lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var donHangs = await _donHangRepository.GetAll();
            return Ok(donHangs);
        }

        // Lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var donHang = await _donHangRepository.GetById(id);
            if (donHang == null)
            {
                return NotFound();
            }
            return Ok(donHang);
        }

        // Thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(DonHang donHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _donHangRepository.Create(donHang);
                    return Ok(donHang);
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
        public async Task<IActionResult> Update(DonHang donHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _donHangRepository.Update(donHang);
                    return Ok(donHang);
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
            var donHang = await _donHangRepository.GetById(id);
            if (donHang == null)
            {
                return NotFound();
            }
            await _donHangRepository.Delete(donHang);
            return Ok();
        }
    }
}
