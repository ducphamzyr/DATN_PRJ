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
    public class KhachHangController : ControllerBase
    {
        private readonly IAllRepositories<KhachHang> _repo;

        public KhachHangController(IAllRepositories<KhachHang> repo)
        {
            _repo = repo;
        }

        // Lấy tất cả khách hàng
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var khachHangs = await _repo.GetAll();
            return Ok(khachHangs);
        }

        // Lấy khách hàng theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var khachHang = await _repo.GetById(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return Ok(khachHang);
        }

        // Thêm khách hàng mới
        [HttpPost("create")]
        public async Task<IActionResult> Create(KhachHang khachHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    khachHang.NgayTao = DateTime.Now; // Đảm bảo ngày tạo được cập nhật hiện tại
                    await _repo.Create(khachHang);
                    return Ok(khachHang);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return BadRequest();
            }
        }

        // Sửa thông tin khách hàng
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(KhachHang khachHang)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    KhachHang khachHangEdit = await _repo.GetById(khachHang.KhachHangID);
                    if (khachHangEdit == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật các thuộc tính
                    khachHangEdit.TenKhachHang = khachHang.TenKhachHang;
                    khachHangEdit.DiaChi = khachHang.DiaChi;
                    khachHangEdit.Email = khachHang.Email;
                    khachHangEdit.SoDienThoai = khachHang.SoDienThoai;
                    khachHangEdit.NgayTao = khachHang.NgayTao; // Có thể cập nhật ngày tạo nếu cần

                    // Không cập nhật TaiKhoans ở đây trừ khi có logic riêng

                    await _repo.Update(khachHangEdit);
                    return Ok(khachHangEdit);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return BadRequest();
            }
        }

        // Xóa khách hàng
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var khachHang = await _repo.GetById(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            try
            {
                await _repo.Delete(khachHang.KhachHangID);
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
