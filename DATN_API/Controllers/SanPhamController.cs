using DATN_API.IRepositories;
using DATN_API.Models;
using DATN_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        IAllRepositories<SanPham> _sanPhamRepository;

        public SanPhamController(AllRepositories<SanPham> sanPhamRepository)
        {
            _sanPhamRepository = sanPhamRepository;
        }
        //lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var sanPhams = await _sanPhamRepository.GetAll();
            return Ok(sanPhams);
        }
        //lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var sanPham = await _sanPhamRepository.GetById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return Ok(sanPham);
        }
        //thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _sanPhamRepository.Create(sanPham);
                    return Ok(sanPham);
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
        public async Task<IActionResult> Update(long id, SanPham sanPham)
        {
            if (id != sanPham.SanPhamID)
            {
                return BadRequest();
            }
            try
            {
                await _sanPhamRepository.Update(sanPham);
                return Ok(sanPham);
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
            var sanPham = await _sanPhamRepository.GetById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            try
            {
                await _sanPhamRepository.Delete(sanPham);
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
