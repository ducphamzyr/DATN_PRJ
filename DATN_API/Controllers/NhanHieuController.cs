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
        private readonly AppDbContext _context;

        public NhanHieuController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/NhanHieus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhanHieu>>> GetNhanHieus()
        {
            if (_context.NhanHieus == null)
            {
                return NotFound();
            }
            return await _context.NhanHieus.ToListAsync();
        }

        // GET: api/NhanHieus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NhanHieu>> GetNhanHieu(long id)
        {
            if (_context.NhanHieus == null)
            {
                return NotFound();
            }
            var nhanHieu = await _context.NhanHieus.FindAsync(id);

            if (nhanHieu == null)
            {
                return NotFound();
            }

            return nhanHieu;
        }

        // PUT: api/NhanHieus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNhanHieu(long id, NhanHieu nhanHieu)
        {
            if (id != nhanHieu.NhanHieuID)
            {
                return BadRequest();
            }

            _context.Entry(nhanHieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanHieuExists(id))
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

        private bool NhanHieuExists(long id)
        {
            return _context.NhanHieus.Any(e => e.NhanHieuID == id);
        }

        // POST: api/NhanHieus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NhanHieu>> PostNhanHieu(NhanHieu nhanHieu)
        {
            _context.NhanHieus.Add(nhanHieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNhanHieu", new { id = nhanHieu.NhanHieuID }, nhanHieu);
        }

        // DELETE: api/NhanHieus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NhanHieu>> DeleteNhanHieu(long id)
        {
            if (_context.NhanHieus == null)
            {
                return NotFound();
            }
            var nhanHieu = await _context.NhanHieus.FindAsync(id);
            if (nhanHieu == null)
            {
                return NotFound();
            }

            _context.NhanHieus.Remove(nhanHieu);
            await _context.SaveChangesAsync();

            return nhanHieu;
        }
    }
}
