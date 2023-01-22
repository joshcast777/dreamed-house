using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamedHouse.Data;
using DreamedHouse.Models;
using Microsoft.AspNetCore.Authorization;

namespace DreamedHouse.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HouseController : ControllerBase
	{
		private readonly AppDbContext _context;

		public HouseController(AppDbContext context)
		{
			_context = context;
		}

		// POST: api/House
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<House>> PostHouse(House house)
		{
			if (_context.Houses == null)
				return Problem("Entity set 'AppDbContext.Users' is null.");

			house.CreatedAt = DateTime.Now;
			house.UpdatedAt = DateTime.Now;

			_context.Houses.Add(house);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetHouse", new { houseId = house.HouseId }, house);
		}

		// GET: api/Houses
		[HttpGet]
		public async Task<ActionResult<IEnumerable<House>>> GetHouses()
		{
			if (_context.Houses == null)
				return NotFound();

			return await _context.Houses
				.Select(house => new House
				{
					HouseId = house.HouseId,
					Name = house.Name,
					Image = house.Image,
					SquareMeters = house.SquareMeters,
					RoomsNumber = house.RoomsNumber,
					FloorsNumber = house.FloorsNumber,
					BathroomsNumber = house.BathroomsNumber,
					Price = house.Price,
					UpdatedAt = house.UpdatedAt
				})
				.ToListAsync();
		}

		// GET: api/House/5
		[HttpGet("{houseId}")]
		public async Task<ActionResult<House>> GetHouse(int houseId)
		{
			if (_context.Houses == null)
				return NotFound();

			var house = await _context.Houses
				.Select(house => new House
				{
					HouseId = house.HouseId,
					Name = house.Name,
					Image = house.Image,
					SquareMeters = house.SquareMeters,
					RoomsNumber = house.RoomsNumber,
					FloorsNumber = house.FloorsNumber,
					BathroomsNumber = house.BathroomsNumber,
					Price = house.Price,
					UpdatedAt = house.UpdatedAt
				})
				.FirstOrDefaultAsync(house => house.HouseId == houseId);

			if (house == null)
				return NotFound();

			return house;
		}

		// PUT: api/House/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{houseId}")]
		[Authorize]
		public async Task<IActionResult> PutHouse(int houseId, House house)
		{
			if (houseId != house.HouseId)
				return BadRequest();

			house.UpdatedAt = DateTime.Now;

			_context.Entry(house).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!HouseExists(houseId))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// DELETE: api/House/5
		[HttpDelete("{houseId}")]
		[Authorize]
		public async Task<IActionResult> DeleteHouse(int houseId)
		{
			if (_context.Houses == null)
				return NotFound();

			var house = await _context.Houses.FindAsync(houseId);

			if (house == null)
				return NotFound();

			_context.Houses.Remove(house);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool HouseExists(int id)
		{
			return (_context.Houses?.Any(e => e.HouseId == id)).GetValueOrDefault();
		}
	}
}
