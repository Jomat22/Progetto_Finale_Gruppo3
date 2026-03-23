using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Client;

public class ClientCreateRequest
{
    [Required(ErrorMessage = "L'ID della persona associata è obbligatorio.")]
    public int PersonId { get; set; }

    [Required(ErrorMessage = "Il Codice Cliente è obbligatorio.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Il Codice Cliente deve avere una lunghezza compresa tra 3 e 20 caratteri.")]
    public string CodiceCliente { get; set; } = default!;

    [Required(ErrorMessage = "Specificare se il cliente è fidelizzato.")]
    public bool IsFidelizzato { get; set; }

    [Required(ErrorMessage = "Specificare se il cliente è iscritto alla newsletter.")]
    public bool IsIscrittoNewsletter { get; set; }
}