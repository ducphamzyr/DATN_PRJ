using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangChiTietController : ControllerBase
    {
        private readonly IAllRepositories<DonHangChiTiet> _donHangChiTietRepository;

        public DonHangChiTietController(IAllRepositories<DonHangChiTiet> donHangChiTietRepository)
        {
            _donHangChiTietRepository = donHangChiTietRepository;
        }

        // Lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var donHangChiTiets = await _donHangChiTietRepository.GetAll();
            return Ok(donHangChiTiets);
        }

        // Lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var donHangChiTiet = await _donHangChiTietRepository.GetById(id);
            if (donHangChiTiet == null)
            {
                return NotFound();
            }
            return Ok(donHangChiTiet);
        }

        // Thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(DonHangChiTiet donHangChiTiet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _donHangChiTietRepository.Create(donHangChiTiet);
                    return Ok(donHangChiTiet);
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
        public async Task<IActionResult> Update(DonHangChiTiet donHangChiTiet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _donHangChiTietRepository.Update(donHangChiTiet);
                    return Ok(donHangChiTiet);
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
            var donHangChiTiet = await _donHangChiTietRepository.GetById(id);
            if (donHangChiTiet == null)
            {
                return NotFound();
            }
            await _donHangChiTietRepository.Delete(donHangChiTiet);
            return Ok();
        }
    }
}
