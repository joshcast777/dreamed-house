using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamedHouse.Data;
using DreamedHouse.Models;
using Microsoft.AspNetCore.Authorization;

namespace DreamedHouse.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProformaInvoiceController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ProformaInvoiceController(AppDbContext context)
		{
			_context = context;
		}

		// POST: api/ProformaInvoice
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<ProformaInvoice>> PostProformaInvoice(ProformaInvoice proformaInvoice)
		{
			if (_context.ProformaInvoices == null)
				return Problem("Entity set 'AppDbContext.ProformaInvoices' is null.");

			_context.ProformaInvoices.Add(proformaInvoice);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProformaInvoice", new { proformaInvoiceId = proformaInvoice.ProformaInvoiceId }, proformaInvoice);
		}

		// GET: api/ProformaInvoices/2
		[HttpGet("/api/ProformaInvoices/{userId}")]
		public async Task<ActionResult<IEnumerable<ProformaInvoice>>> GetProformaInvoices(int userId)
		{
			if (_context.ProformaInvoices == null)
				return NotFound();

			return await _context.ProformaInvoices
				.Where(proformaInvoice => proformaInvoice.UserId == userId)
				.Select(proformaInvoice => new ProformaInvoice
				{
					ProformaInvoiceId = proformaInvoice.ProformaInvoiceId,
					Total = proformaInvoice.Total,
					House = new House
					{
						HouseId = proformaInvoice.House.HouseId,
						Name = proformaInvoice.House.Name,
						Image = proformaInvoice.House.Image,
						SquareMeters = proformaInvoice.House.SquareMeters,
						RoomsNumber = proformaInvoice.House.RoomsNumber,
						FloorsNumber = proformaInvoice.House.FloorsNumber,
						BathroomsNumber = proformaInvoice.House.BathroomsNumber,
						Price = proformaInvoice.House.Price
					},
					ProformaInvoicesFinishingHouses = proformaInvoice.ProformaInvoicesFinishingHouses
						.Select(proformaInvoiceFinishingHouse => new ProformaInvoiceFinishingHouse
						{
							FinishingHouse = new FinishingHouse
							{
								FinishingHouseId = proformaInvoiceFinishingHouse.FinishingHouseId,
								Name = proformaInvoiceFinishingHouse.FinishingHouse.Name,
								Price = proformaInvoiceFinishingHouse.FinishingHouse.Price
							}
						})
				})
				.ToListAsync();
		}

		// GET: api/ProformaInvoice/5
		[HttpGet("{proformaInvoiceId}")]
		public async Task<ActionResult<ProformaInvoice>> GetProformaInvoice(int proformaInvoiceId)
		{
			if (_context.ProformaInvoices == null)
				return NotFound();

			var proformaInvoice = await _context.ProformaInvoices
				.Select(proformaInvoice => new ProformaInvoice
				{
					ProformaInvoiceId = proformaInvoice.ProformaInvoiceId,
					Total = proformaInvoice.Total,
					House = new House
					{
						HouseId = proformaInvoice.House.HouseId,
						Name = proformaInvoice.House.Name,
						Image = proformaInvoice.House.Image,
						SquareMeters = proformaInvoice.House.SquareMeters,
						RoomsNumber = proformaInvoice.House.RoomsNumber,
						FloorsNumber = proformaInvoice.House.FloorsNumber,
						BathroomsNumber = proformaInvoice.House.BathroomsNumber,
						Price = proformaInvoice.House.Price
					},
					ProformaInvoicesFinishingHouses = proformaInvoice.ProformaInvoicesFinishingHouses
						.Select(proformaInvoiceFinishingHouse => new ProformaInvoiceFinishingHouse
						{
							FinishingHouse = new FinishingHouse
							{
								FinishingHouseId = proformaInvoiceFinishingHouse.FinishingHouseId,
								Name = proformaInvoiceFinishingHouse.FinishingHouse.Name,
								Price = proformaInvoiceFinishingHouse.FinishingHouse.Price
							}
						})
				})
				.FirstOrDefaultAsync(proformaInvoice => proformaInvoice.ProformaInvoiceId == proformaInvoiceId);

			if (proformaInvoice == null)
				return NotFound();

			return proformaInvoice;
		}

		// PUT: api/ProformaInvoice/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProformaInvoice(int id, ProformaInvoice proformaInvoice)
		{
			if (id != proformaInvoice.ProformaInvoiceId)
				return BadRequest();

			_context.Entry(proformaInvoice).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProformaInvoiceExists(id))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// DELETE: api/ProformaInvoice/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProformaInvoice(int id)
		{
			if (_context.ProformaInvoices == null)
				return NotFound();

			var proformaInvoice = await _context.ProformaInvoices.FindAsync(id);

			if (proformaInvoice == null)
				return NotFound();

			_context.ProformaInvoices.Remove(proformaInvoice);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProformaInvoiceExists(int id)
		{
			return (_context.ProformaInvoices?.Any(e => e.ProformaInvoiceId == id)).GetValueOrDefault();
		}
	}
}
