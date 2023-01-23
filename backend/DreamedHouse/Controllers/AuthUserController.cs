using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DreamedHouse.Data;
using DreamedHouse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DreamedHouse.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthUserController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IConfiguration _configuration;

		public AuthUserController(AppDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		// POST: api/AuthUser/SignIn
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost("SignIn")]
		public async Task<ActionResult> PostAuthUser(AuthUser authUser)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == authUser.Email && u.Password == authUser.Password);

			if (user == null) return BadRequest("InvalidEmailOrPassword");
			else return Ok(JsonConvert.SerializeObject(GenerateToken(user)));
		}

		// POST: api/AuthUser/SignUp
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost("SignUp")]
		public async Task<ActionResult<User>> PostUser(User user)
		{
			if (_context.Users == null)
				return Problem("Entity set 'AppDbContext.Users' is null.");

			user.RoleId = 2;
			user.CreatedAt = DateTime.Now;
			user.UpdatedAt = DateTime.Now;

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return Ok(JsonConvert.SerializeObject(GenerateToken(user)));
		}

		private string GenerateToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Email, user.Email)
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = System.DateTime.Now.AddDays(1),
				SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}
	}
}
