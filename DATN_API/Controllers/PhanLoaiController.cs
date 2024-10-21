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
        private readonly AppDbContext _context;

        public PhanLoaiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PhanLoais
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanLoai>>> GetPhanLoais()
        {
            if (_context.PhanLoais == null)
            {
                return NotFound();
            }
            return await _context.PhanLoais.ToListAsync();
        }

        // GET: api/PhanLoais/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhanLoai>> GetPhanLoai(long id)
        {
            if (_context.PhanLoais == null)
            {
                return NotFound();
            }
            var phanLoai = await _context.PhanLoais.FindAsync(id);

            if (phanLoai == null)
            {
                return NotFound();
            }

            return phanLoai;
        }

        // PUT: api/PhanLoais/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhanLoai(long id, PhanQuyen phanQuyen)
        {
            if (id != phanQuyen.PhanQuyenID)
            {
                return BadRequest();
            }

            _context.Entry(phanQuyen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhanLoaiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PhanLoaiExists(long id)
        {
            return _context.PhanLoais.Any(e => e.PhanLoaiID == id);
        }

        // POST: api/PhanLoais
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhanLoai>> PostPhanLoai(PhanLoai phanLoai)
        {
            _context.PhanLoais.Add(phanLoai);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhanLoai", new { id = phanLoai.PhanLoaiID }, phanLoai);
        }

        // DELETE: api/PhanLoais/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhanLoai(long id)
        {
            if (_context.PhanLoais == null)
            {
                return NotFound();
            }
            var phanLoai = await _context.PhanLoais.FindAsync(id);
            if (phanLoai == null)
            {
                return NotFound();
            }

            _context.PhanLoais.Remove(phanLoai);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
