using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamedHouse.Data;
using DreamedHouse.Models;

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

		// GET: api/House
		[HttpGet]
		public async Task<ActionResult<IEnumerable<House>>> GetHouses()
		{
			if (_context.Houses == null)
				return NotFound();

			return await _context.Houses
				.Select(house => new House{
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
		[HttpGet("{id}")]
		public async Task<ActionResult<House>> GetHouse(int id)
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
				.FirstOrDefaultAsync(house => house.HouseId == id);

			if (house == null)
				return NotFound();

			return house;
		}
	}
}
