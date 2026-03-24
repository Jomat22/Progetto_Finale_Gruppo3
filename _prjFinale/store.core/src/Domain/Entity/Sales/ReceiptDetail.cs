using store.core.src.Domain.Entity.Catalog;
namespace store.core.src.Domain.Entity.Sales;

public class ReceiptDetail : BaseEntity
{
    public int RicevutaId { get; set; }
    public virtual Receipt Ricevuta { get; set; } = default!;

    public int ProdottoId { get; set; }
    // Navigazione verso il prodotto per facilitare i calcoli
    public virtual Product Prodotto { get; set; } = default!;

    public int Quantita { get; set; }
    public decimal PrezzoTotale { get; set; }
}