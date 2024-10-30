using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaGiamGiaController : ControllerBase
    {
        private readonly IAllRepositories<MaGiamGia> _maGiamGiaRepository;

        public MaGiamGiaController(IAllRepositories<MaGiamGia> maGiamGiaRepository)
        {
            _maGiamGiaRepository = maGiamGiaRepository;
        }

        // Lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var maGiamGias = await _maGiamGiaRepository.GetAll();
            return Ok(maGiamGias);
        }

        // Lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var maGiamGia = await _maGiamGiaRepository.GetById(id);
            if (maGiamGia == null)
            {
                return NotFound();
            }
            return Ok(maGiamGia);
        }

        // Thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(MaGiamGia maGiamGia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _maGiamGiaRepository.Create(maGiamGia);
                    return Ok(maGiamGia);
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
        public async Task<IActionResult> Update(MaGiamGia maGiamGia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _maGiamGiaRepository.Update(maGiamGia);
                    return Ok(maGiamGia);
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
            var maGiamGia = await _maGiamGiaRepository.GetById(id);
            if (maGiamGia == null)
            {
                return NotFound();
            }
            await _maGiamGiaRepository.Delete(maGiamGia);
            return Ok();
        }


    }
}
