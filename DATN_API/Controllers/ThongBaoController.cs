using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongBaoController : ControllerBase
    {
        private readonly IAllRepositories<ThongBao> _thongBaoRepository;

        public ThongBaoController(IAllRepositories<ThongBao> thongBaoRepository)
        {
            _thongBaoRepository = thongBaoRepository;
        }

        // Lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var thongBao = await _thongBaoRepository.GetAll();
            return Ok(thongBao);
        }

        // Lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var thongBao = await _thongBaoRepository.GetById(id);
            if (thongBao == null)
            {
                return NotFound();
            }
            return Ok(thongBao);
        }

        // Thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(ThongBao thongBao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _thongBaoRepository.Create(thongBao);
                    return Ok(thongBao);
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
        public async Task<IActionResult> Update(ThongBao thongBao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _thongBaoRepository.Update(thongBao);
                    return Ok(thongBao);
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
            var thongBao = await _thongBaoRepository.GetById(id);
            if (thongBao == null)
            {
                return NotFound();
            }
            await _thongBaoRepository.Delete(thongBao);
            return Ok();
        }

        


    }
}
