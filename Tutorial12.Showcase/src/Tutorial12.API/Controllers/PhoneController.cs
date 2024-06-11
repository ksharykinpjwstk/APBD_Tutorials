using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tutorial12.API.DTOs.Phones;
using Tutorial12.API.Entities;
using Tutorial12.API.Helpers;

namespace Tutorial12.API.Controllers
{
    [Route("api/phone")]
    [Authorize]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PhoneController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Phone
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PhoneDto>>> GetPhones()
        {
            var dbPhones = await _context.Phones.Include(p => p.PhoneManufacture).ToListAsync();

            return dbPhones.Select(phone => new PhoneDto(phone)).ToList();
        }

        // GET: api/Phone/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Phone>> GetPhone(int id)
        {
            var phone = await _context.Phones.FindAsync(id);

            if (phone == null)
            {
                return NotFound();
            }

            return phone;
        }

        // PUT: api/Phone/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutPhone(PhoneDto updatedPhone, CancellationToken cancellationToken)
        {
            if (!PhoneExists(updatedPhone.Id))
            {
                return BadRequest();
            }

            var manufacture =
                await _context.PhoneManufactures.FirstOrDefaultAsync(pm => string.Equals(pm.Name, updatedPhone.Manufacture),
                    cancellationToken: cancellationToken);
            if (manufacture is null)
            {
                return BadRequest("Manufacture with given name was not found.");
            }
            
            var phone = await _context.Phones.FindAsync(new object?[] { updatedPhone.Id }, cancellationToken: cancellationToken);
            phone = updatedPhone.Map(manufacture.Id);
            _context.Entry(phone).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);

            return NoContent();
        }

        // POST: api/Phone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Phone>> PostPhone(PhoneDto newPhone, CancellationToken cancellationToken)
        {
            var manufacture =
                await _context.PhoneManufactures.FirstOrDefaultAsync(pm => string.Equals(pm.Name, newPhone.Manufacture),
                    cancellationToken: cancellationToken);
            if (manufacture is null)
            {
                return BadRequest("Manufacture with given name was not found.");
            }

            var mappedNewPhone = newPhone.Map(manufacture.Id);
            _context.Phones.Add(mappedNewPhone);
            await _context.SaveChangesAsync(cancellationToken);

            return CreatedAtAction("GetPhone", new { id = mappedNewPhone.Id }, mappedNewPhone);
        }

        // DELETE: api/Phone/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneExists(int id)
        {
            return _context.Phones.Any(e => e.Id == id);
        }
    }
}