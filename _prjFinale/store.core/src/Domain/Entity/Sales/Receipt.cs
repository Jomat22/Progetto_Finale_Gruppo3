using store.core.src.Domain.Entity.User;

namespace store.core.src.Domain.Entity.Sales;

public class Receipt : BaseEntity
{
    public int ClientId { get; set; }
    public virtual Client? Client { get; set; }
    public DateTime DataEmissione { get; set; }
    public decimal TotaleDefinitivo { get; set; }
    public string MetodoPagamento { get; set; } = default!;

    public virtual List<ReceiptDetail> RicevutaDettagli { get; set; } = [];
}