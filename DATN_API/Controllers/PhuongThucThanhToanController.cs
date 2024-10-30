using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhuongThucThanhToanController : ControllerBase
    {
        private readonly IAllRepositories<PhuongThucThanhToan> _phuongThucThanhToanRepository;

        public PhuongThucThanhToanController(IAllRepositories<PhuongThucThanhToan> phuongThucThanhToanRepository)
        {
            _phuongThucThanhToanRepository = phuongThucThanhToanRepository;
        }

        // Lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var phuongThucThanhToans = await _phuongThucThanhToanRepository.GetAll();
            return Ok(phuongThucThanhToans);
        }

        // Lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var phuongThucThanhToan = await _phuongThucThanhToanRepository.GetById(id);
            if (phuongThucThanhToan == null)
            {
                return NotFound();
            }
            return Ok(phuongThucThanhToan);
        }

        // Thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(PhuongThucThanhToan phuongThucThanhToan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _phuongThucThanhToanRepository.Create(phuongThucThanhToan);
                    return Ok(phuongThucThanhToan);
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

        public async Task<IActionResult> Update(PhuongThucThanhToan phuongThucThanhToan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _phuongThucThanhToanRepository.Update(phuongThucThanhToan);
                    return Ok(phuongThucThanhToan);
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
            var phuongThucThanhToan = await _phuongThucThanhToanRepository.GetById(id);
            if (phuongThucThanhToan == null)
            {
                return NotFound();
            }
            await _phuongThucThanhToanRepository.Delete(phuongThucThanhToan);
            return Ok();
        }
    }
}
