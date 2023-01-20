namespace DreamedHouse.Models;

public partial class User
{
	public int UserId { get; set; }

	public string Dni { get; set; } = null!;

	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;

	public DateOnly BirthDate { get; set; }

	public string PhoneNumber { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string Password { get; set; } = null!;

	public int RoleId { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public virtual Role Role { get; set; } = null!;
}
