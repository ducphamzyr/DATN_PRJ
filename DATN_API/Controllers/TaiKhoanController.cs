using DATN_API.IRepositories;
using DATN_API.Models;
using Microsoft.AspNetCore.Mvc;


namespace DATN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly IAllRepositories<TaiKhoan> _repo;

        public TaiKhoanController(IAllRepositories<TaiKhoan> repo)
        {
            _repo = repo;
        }

        // Lấy tất cả tài khoản
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var taiKhoans = await _repo.GetAll();
            return Ok(taiKhoans);
        }

        // Lấy tài khoản theo id
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var taiKhoan = await _repo.GetById(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            return Ok(taiKhoan);
        }

        // Thêm tài khoản mới
        [HttpPost("create")]
        public async Task<IActionResult> Create(TaiKhoan taiKhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    taiKhoan.CreateAt = DateTime.Now; // Thiết lập ngày tạo
                    await _repo.Create(taiKhoan);
                    return Ok(taiKhoan);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return BadRequest();
            }
        }

        // Sửa thông tin tài khoản
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(TaiKhoan taiKhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TaiKhoan taiKhoanEdit = await _repo.GetById(taiKhoan.TaiKhoanID);
                    if (taiKhoanEdit == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật các thuộc tính của tài khoản
                    taiKhoanEdit.TenDangNhap = taiKhoan.TenDangNhap;
                    taiKhoanEdit.MatKhauHash = taiKhoan.MatKhauHash;
                    taiKhoanEdit.KhachHangID = taiKhoan.KhachHangID;
                    taiKhoanEdit.GioHangID = taiKhoan.GioHangID;
                    taiKhoanEdit.PhanQuyenID = taiKhoan.PhanQuyenID;
                    taiKhoanEdit.CreateAt = taiKhoan.CreateAt;

                    await _repo.Update(taiKhoanEdit);
                    return Ok(taiKhoanEdit);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException?.Message ?? e.Message);
                return BadRequest();
            }
        }

        // Xóa tài khoản
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var taiKhoan = await _repo.GetById(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            try
            {
                await _repo.Delete(taiKhoan.TaiKhoanID);
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
