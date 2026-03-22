using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Product;

public class ProductUpdateRequest
{
    // 'Sku' usato ai fini di verifica esistenza
    [Required(ErrorMessage = "Lo SKU è obbligatorio.")]
    [StringLength(30, ErrorMessage = "Lo SKU non può superare i 30 caratteri.")]
    public string Sku { get; set; } = default!;

    [Required(ErrorMessage = "Il Nome del prodotto è obbligatorio.")]
    [StringLength(100, ErrorMessage = "Il Nome non può superare i 100 caratteri.")]
    public string Nome { get; set; } = default!;

    [Required(ErrorMessage = "La quantità è obbligatoria.")]
    [Range(0, int.MaxValue, ErrorMessage = "La quantità non può essere negativa.")]
    public int Qnt { get; set; }
}