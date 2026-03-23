using System.ComponentModel.DataAnnotations;
namespace store.core.src.Domain.Entity.Catalog;

public class Receipt : BaseEntity
{
    public DateTime DataEmissione { get; set; }
    public decimal TotaleDefinitivo { get; set; }
    public string MetodoPagamento { get; set; } = default!;

    public virtual List<ReceiptDetail> RicevutaDettagli { get; set; } = [];
}