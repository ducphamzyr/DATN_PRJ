using DATN_API.IRepositories;
using DATN_API.Models;
using DATN_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanLoaiController : ControllerBase
    {
        private readonly IAllRepositories<PhanLoai> _phanLoaiRepository;

        public PhanLoaiController(IAllRepositories<PhanLoai> phanLoaiRepository)
        {
            _phanLoaiRepository = phanLoaiRepository;
        }

        //lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var phanLoais = await _phanLoaiRepository.GetAll();
            return Ok(phanLoais);
        }

        //lấy theo id

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var phanLoai = await _phanLoaiRepository.GetById(id);
            if (phanLoai == null)
            {
                return NotFound();
            }
            return Ok(phanLoai);
        }

        //thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(PhanLoai phanLoai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _phanLoaiRepository.Create(phanLoai);
                    return Ok(phanLoai);
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
        public async Task<IActionResult> Update(PhanLoai phanLoai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _phanLoaiRepository.Update(phanLoai);
                    return Ok(phanLoai);
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
                var phanLoai = await _phanLoaiRepository.GetById(id);
                if (phanLoai == null)
                {
                    return NotFound();
                }
                await _phanLoaiRepository.Delete(phanLoai);
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
