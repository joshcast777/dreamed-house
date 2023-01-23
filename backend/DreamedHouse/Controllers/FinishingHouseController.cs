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
	public class FinishingHouseController : ControllerBase
	{
		private readonly AppDbContext _context;

		public FinishingHouseController(AppDbContext context)
		{
			_context = context;
		}

		// POST: api/FinishingHouse
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<FinishingHouse>> PostFinishingHouse(FinishingHouse finishingHouse)
		{
			if (_context.FinishingHouses == null)
				return Problem("Entity set 'AppDbContext.Users' is null.");

			finishingHouse.CreatedAt = DateTime.Now;
			finishingHouse.UpdatedAt = DateTime.Now;

			_context.FinishingHouses.Add(finishingHouse);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetFinishingHouse", new { id = finishingHouse.FinishingHouseId }, finishingHouse);
		}

		// GET: api/FinishingHouses
		[HttpGet]
		public async Task<ActionResult<IEnumerable<FinishingHouse>>> GetFinishingHouses()
		{
			if (_context.FinishingHouses == null)
				return NotFound();

			return await _context.FinishingHouses
				.Select(finishingHouse => new FinishingHouse
				{
					FinishingHouseId = finishingHouse.FinishingHouseId,
					Name = finishingHouse.Name,
					Price = finishingHouse.Price,
					TypeFinishing = finishingHouse.TypeFinishing
				})
				.ToListAsync();
		}

		// PUT: api/FinishingHouse/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{finishingHouseId}")]
		public async Task<IActionResult> PutFinishingHouse(int finishingHouseId, FinishingHouse finishingHouse)
		{
			if (finishingHouseId != finishingHouse.FinishingHouseId)
				return BadRequest();

			finishingHouse.UpdatedAt = DateTime.Now;

			_context.Entry(finishingHouse).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FinishingHouseExists(finishingHouseId))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// DELETE: api/FinishingHouse/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteFinishingHouse(int finishingHouseId)
		{
			if (_context.FinishingHouses == null)
				return NotFound();

			var finishingHouse = await _context.FinishingHouses.FindAsync(finishingHouseId);

			if (finishingHouse == null)
				return NotFound();

			_context.FinishingHouses.Remove(finishingHouse);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool FinishingHouseExists(int finishingHouseId)
		{
			return (_context.FinishingHouses?.Any(e => e.FinishingHouseId == finishingHouseId)).GetValueOrDefault();
		}
	}
}
