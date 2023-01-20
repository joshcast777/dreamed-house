using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DreamedHouse.Data;
using DreamedHouse.Models;

namespace DreamedHouse.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FinishingHouseController : ControllerBase
	{
		private readonly AppDbContext _context;

		public FinishingHouseController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/FinishingHouse
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
	}
}
