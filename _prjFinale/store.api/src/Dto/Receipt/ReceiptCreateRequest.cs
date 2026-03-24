using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Receipt;

public class ReceiptCreateRequest
{
    [Required(ErrorMessage = "Il metodo di pagamento è obbligatorio.")]
    [StringLength(50, ErrorMessage = "Il metodo di pagamento non può superare i 50 caratteri.")]
    public string MetodoPagamento { get; set; } = default!;

    [Required(ErrorMessage = "Lista prodotti obbligatoria.")]
    [MinLength(1, ErrorMessage = "Inserire almeno un prodotto.")]
    public List<ReceiptDetailRequest> Prodotti { get; set; } = default!;
}

public class ReceiptDetailRequest
{
    [Required(ErrorMessage = "L'ID del prodotto è obbligatorio.")]
    public int ProdottoId { get; set; }

    [Required(ErrorMessage = "Quantità prodotto obbligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La quantità minima necessaria è 1.")]
    public int Quantita { get; set; }

    //Decorator
    public bool GiftWrap { get; set; } = false;
    public bool Express { get; set; } = false;
    public bool Assicurazione { get; set; } = false;
}

