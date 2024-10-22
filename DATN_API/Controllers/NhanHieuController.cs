using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanHieuController : ControllerBase
    {
        private readonly IAllRepositories<NhanHieu> _nhanHieuRepository;

        public NhanHieuController(IAllRepositories<NhanHieu> nhanHieuRepository)
        {
            _nhanHieuRepository = nhanHieuRepository;
        }

        //lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var nhanHieus = await _nhanHieuRepository.GetAll();
            return Ok(nhanHieus);
        }

        //lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var nhanHieu = await _nhanHieuRepository.GetById(id);
            if (nhanHieu == null)
            {
                return NotFound();
            }
            return Ok(nhanHieu);
        }

        //thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(NhanHieu nhanHieu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _nhanHieuRepository.Create(nhanHieu);
                    return Ok(nhanHieu);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message, e.Message);
                return BadRequest();
            }
        }

        //sửa
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(NhanHieu nhanHieu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _nhanHieuRepository.Update(nhanHieu);
                    return Ok(nhanHieu);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message, e.Message);
                return BadRequest();
            }
        }

        //xóa
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var nhanHieu = await _nhanHieuRepository.GetById(id);
                if (nhanHieu == null)
                {
                    return NotFound();
                }
                await _nhanHieuRepository.Delete(nhanHieu);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message, e.Message);
                return BadRequest();
            }
        }
    }
}
