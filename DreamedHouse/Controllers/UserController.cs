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

		// POST: api/User
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<User>> PostUser(User user)
		{
			if (_context.Users == null)
				return Problem("Entity set 'AppDbContext.Users' is null.");

			user.RoleId = 3;
			user.CreatedAt = DateTime.Now;
			user.UpdatedAt = DateTime.Now;

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetUser", new { id = user.UserId }, user);
		}

		// GET: api/User
		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			if (_context.Users == null)
				return NotFound();

			return await _context.Users
				.Where(user => user.RoleId == 2)
				.ToListAsync();
		}

		// GET: api/User/5
		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			if (_context.Users == null)
				return NotFound();

			var user = await _context.Users
				.Select(user => new User{
					UserId = user.UserId,
					Dni = user.Dni,
					FirstName = user.FirstName,
					LastName = user.LastName,
					BirthDate = user.BirthDate,
					PhoneNumber = user.PhoneNumber,
					Email = user.Email,
					Password = user.Password,
					RoleId = user.RoleId,
					CreatedAt = user.CreatedAt,
					UpdatedAt = user.UpdatedAt
				})
				.FirstOrDefaultAsync(user => user.UserId == id);

			if (user == null)
				return NotFound();

			return user;
		}

		// PUT: api/User/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutUser(int id, User user)
		{
			if (id != user.UserId)
				return BadRequest();

			user.UpdatedAt = DateTime.Now;

			_context.Entry(user).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(id))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		// DELETE: api/User/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			if (_context.Users == null)
				return NotFound();

			var user = await _context.Users.FindAsync(id);

			if (user == null)
				return NotFound();

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool UserExists(int id)
		{
			return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
		}
	}
}
