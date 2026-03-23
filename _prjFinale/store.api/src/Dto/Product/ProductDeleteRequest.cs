using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Product;

public class ProductDeleteRequest
{
    [Required(ErrorMessage = "Lo SKU è obbligatorio.")]
    [StringLength(30, ErrorMessage = "Lo SKU non può superare i 30 caratteri.")]
    public string Sku { get; set; } = default!;
}