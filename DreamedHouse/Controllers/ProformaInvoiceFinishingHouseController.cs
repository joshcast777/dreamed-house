using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamedHouse.Data;
using DreamedHouse.Models;

namespace DreamedHouse.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProformaInvoiceFinishingHouseController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ProformaInvoiceFinishingHouseController(AppDbContext context)
		{
			_context = context;
		}
		// POST: api/ProformaInvoiceFinishingHouse
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<ProformaInvoiceFinishingHouse>> PostProformaInvoiceFinishingHouse(ProformaInvoiceFinishingHouse proformaInvoiceFinishingHouse)
		{
			if (_context.ProformaInvoiceFinishingHouse == null)
				return Problem("Entity set 'AppDbContext.ProformaInvoiceFinishingHouse'  is null.");

			_context.ProformaInvoiceFinishingHouse.Add(proformaInvoiceFinishingHouse);
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				if (ProformaInvoiceFinishingHouseExists(proformaInvoiceFinishingHouse.ProformaInvoiceId))
					return Conflict();
				else
					throw;
			}

			return CreatedAtAction("GetProformaInvoiceFinishingHouse", new { id = proformaInvoiceFinishingHouse.ProformaInvoiceId }, proformaInvoiceFinishingHouse);
		}

		// PUT: api/ProformaInvoiceFinishingHouse/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProformaInvoiceFinishingHouse(int id, ProformaInvoiceFinishingHouse proformaInvoiceFinishingHouse)
		{
			if (id != proformaInvoiceFinishingHouse.ProformaInvoiceId)
				return BadRequest();

			_context.Entry(proformaInvoiceFinishingHouse).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProformaInvoiceFinishingHouseExists(id))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// DELETE: api/ProformaInvoiceFinishingHouse/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProformaInvoiceFinishingHouse(int id)
		{
			if (_context.ProformaInvoiceFinishingHouse == null)
				return NotFound();

			var proformaInvoiceFinishingHouse = await _context.ProformaInvoiceFinishingHouse.FindAsync(id);

			if (proformaInvoiceFinishingHouse == null)
				return NotFound();

			_context.ProformaInvoiceFinishingHouse.Remove(proformaInvoiceFinishingHouse);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProformaInvoiceFinishingHouseExists(int id)
		{
			return (_context.ProformaInvoiceFinishingHouse?.Any(e => e.ProformaInvoiceId == id)).GetValueOrDefault();
		}
	}
}
