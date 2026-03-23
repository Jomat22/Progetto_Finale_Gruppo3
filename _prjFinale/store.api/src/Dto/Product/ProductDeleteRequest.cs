using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Product;

public class ProductDeleteRequest
{
    [Required(ErrorMessage = "Lo SKU è obbligatorio.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Lo SKU deve avere una lunghezza compresa tra 3 e 20 caratteri.")]
    public string Sku { get; set; } = default!;
}