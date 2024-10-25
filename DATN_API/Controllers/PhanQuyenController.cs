using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATN_API;
using DATN_API.Models;
using DATN_API.IRepositories;

namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanQuyenController : ControllerBase
    {
        private readonly IAllRepositories<PhanQuyen> _repo;

        public PhanQuyenController(IAllRepositories<PhanQuyen> repo)
        {
            _repo = repo;
        }

        // Lấy tất cả
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var phanQuyen = await _repo.GetAll();
            return Ok(phanQuyen);
        }

        // Lấy theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var phanQuyen = await _repo.GetById(id);
            if (phanQuyen == null)
            {
                return NotFound();
            }
            return Ok(phanQuyen);
        }

        // Thêm
        [HttpPost("create")]
        public async Task<IActionResult> Create(PhanQuyen phanQuyen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repo.Create(phanQuyen);
                    return Ok(phanQuyen);
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
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(PhanQuyen phanQuyen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PhanQuyen phanQuyenEdit = await _repo.GetById(phanQuyen.PhanQuyenID);
                    if (phanQuyenEdit == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật các thuộc tính của PhanQuyen
                    phanQuyenEdit.TenPhanQuyen = phanQuyen.TenPhanQuyen;

                    await _repo.Update(phanQuyenEdit);
                    return Ok(phanQuyenEdit);
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
            var phanQuyen = await _repo.GetById(id);
            if (phanQuyen == null)
            {
                return NotFound();
            }
            try
            {
                await _repo.Delete(phanQuyen.PhanQuyenID);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return BadRequest();
            }
        }
    }

}
