using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Product;

public class ProductUpdateRequest
{
    // 'Sku' usato ai fini di verifica esistenza
    [Required(ErrorMessage = "Lo SKU è obbligatorio.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Lo SKU deve avere una lunghezza compresa tra 3 e 20 caratteri.")]
    public string Sku { get; set; } = default!;

    [Required(ErrorMessage = "Il Nome del prodotto è obbligatorio.")]
    [StringLength(100, ErrorMessage = "Il Nome non può superare i 100 caratteri.")]
    public string Nome { get; set; } = default!;

    [Required(ErrorMessage = "Il prezzo è obbligatorio.")]
    [Range(0, 99999999.99, ErrorMessage = "Il prezzo inserito non è valido.")]
    public decimal Prezzo { get; set; }

    [Required(ErrorMessage = "La quantità è obbligatoria.")]
    [Range(0, int.MaxValue, ErrorMessage = "La quantità inserita non è valida.")]
    public int Quantita { get; set; }
}