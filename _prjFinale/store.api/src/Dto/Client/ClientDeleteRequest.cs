using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Client;

public class ClientDeleteRequest
{
    [Required(ErrorMessage = "Il Codice Cliente è obbligatorio.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Il Codice Cliente deve avere una lunghezza compresa tra 3 e 20 caratteri.")]
    public string CodiceCliente { get; set; } = default!;
}