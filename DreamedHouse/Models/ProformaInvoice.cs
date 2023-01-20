namespace DreamedHouse.Models;

public partial class ProformaInvoice
{
	public int ProformaInvoiceId { get; set; }

	public double Total { get; set; }

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public int UserId { get; set; }

	public int HouseId { get; set; }

	public virtual House House { get; set; } = null!;

	public virtual User User { get; set; } = null!;

	public virtual IEnumerable<ProformaInvoiceFinishingHouse> ProformaInvoicesFinishingHouses { get; set; } = new List<ProformaInvoiceFinishingHouse>();
}
