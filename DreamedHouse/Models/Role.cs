namespace DreamedHouse.Models;

public partial class Role
{
	public int RoleId { get; set; }

	public string Name { get; set; } = null!;

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public virtual ICollection<User> Users { get; } = new List<User>();
}
