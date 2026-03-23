using System.ComponentModel.DataAnnotations;
using store.core.src.Domain.Entity.Catalog;
namespace store.api.src.Dto.Receipt;

public class ScontrinoCreateRequest
{
    public DateTime DataEmissione { get; set; } = DateTime.UtcNow;
    public string MetodoPagamento { get; set; } = default!;
    public decimal TotaleDefinitivo { get; set; }
    
    public List<ReceiptDetail> Prodotti { get; set; } = [];
}