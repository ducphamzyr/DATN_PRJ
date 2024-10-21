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
        private readonly IAllRepositories<SanPham> _sanPhamRepository;

        public SanPhamController(IAllRepositories<SanPham> sanPhamRepository)
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
        public async Task<IActionResult> Update(SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SanPham sanPhamEdit = await _sanPhamRepository.GetById(sanPham.SanPhamID);
                    if (sanPhamEdit == null)
                    {
                        return NotFound();
                    }
                    sanPhamEdit.TenSanPham = sanPham.TenSanPham;
                    sanPhamEdit.NhanHieuID = sanPham.NhanHieuID;
                    sanPhamEdit.NgayKhoiTao = sanPham.NgayKhoiTao;
                    sanPhamEdit.TrangThai = sanPham.TrangThai;
                    sanPhamEdit.GhiChu = sanPham.GhiChu;
                    sanPhamEdit.PhanLoaiID = sanPham.PhanLoaiID;
                    sanPhamEdit.Mau = sanPham.Mau;
                    sanPhamEdit.BoNho = sanPham.BoNho;
                    sanPhamEdit.KheSim = sanPham.KheSim;
                    sanPhamEdit.Ram = sanPham.Ram;
                    sanPhamEdit.Gia = sanPham.Gia;
                    sanPhamEdit.ConLai = sanPham.ConLai;

                    await _sanPhamRepository.Update(sanPhamEdit);
                    return Ok(sanPhamEdit);
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
            var sanPham = await _sanPhamRepository.GetById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            try
            {
                await _sanPhamRepository.Delete(sanPham.SanPhamID);
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
