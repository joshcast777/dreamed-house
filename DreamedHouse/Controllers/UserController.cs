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
	public class UserController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UserController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/User/5
		[HttpGet("{userId}")]
		public async Task<ActionResult<User>> GetUser(int userId)
		{
			if (_context.Users == null)
				return NotFound();

			var user = await _context.Users.FindAsync(userId);

			if (user == null)
				return NotFound();

			return user;
		}

		// PUT: api/User/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{userId}")]
		public async Task<IActionResult> PutUser(int userId, User user)
		{
			if (userId != user.UserId)
				return BadRequest();

			user.UpdatedAt = DateTime.Now;

			_context.Entry(user).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(userId))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// DELETE: api/User/5
		[HttpDelete("{userId}")]
		public async Task<IActionResult> DeleteUser(int userId)
		{
			if (_context.Users == null)
				return NotFound();

			var user = await _context.Users.FindAsync(userId);

			if (user == null)
				return NotFound();

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool UserExists(int userId)
		{
			return (_context.Users?.Any(e => e.UserId == userId)).GetValueOrDefault();
		}
	}
}
