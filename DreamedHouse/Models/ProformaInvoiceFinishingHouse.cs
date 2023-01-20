namespace DreamedHouse.Models;

public partial class ProformaInvoiceFinishingHouse
{
	public int ProformaInvoiceId { get; set; }

	public int FinishingHouseId { get; set; }

	public virtual ProformaInvoice ProformaInvoice { get; set; } = null!;

	public virtual FinishingHouse FinishingHouse { get; set; } = null!;
}
