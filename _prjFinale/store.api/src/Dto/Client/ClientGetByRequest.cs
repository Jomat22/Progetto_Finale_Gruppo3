using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Client;

public class ClientGetByRequest
{
    [Required(ErrorMessage = "Il Codice Cliente è obbligatorio.")]
    [StringLength(20, ErrorMessage = "Il Codice Cliente non può superare i 20 caratteri.")]
    public string CodiceCliente { get; set; } = default!;
}