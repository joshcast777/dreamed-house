namespace DreamedHouse.Models;

public partial class FinishingHouse
{
	public int FinishingHouseId { get; set; }

	public string Name { get; set; } = null!;

	public double Price { get; set; }

	public string TypeFinishing { get; set; } = null!;

	public DateTime CreatedAt { get; set; }

	public DateTime UpdatedAt { get; set; }

	public virtual IEnumerable<ProformaInvoiceFinishingHouse> ProformaInvoicesFinishingHouses { get; set; } = new List<ProformaInvoiceFinishingHouse>();
}
