using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Client;

public class ClientUpdateRequest
{
    // Attualmente 'PersonId' non è modificabile
    /* [Required(ErrorMessage = "L'ID della persona associata è obbligatorio.")]
    public int PersonId { get; set; } */

    // 'CodiceCliente' usato ai fini di verifica esistenza
    [Required(ErrorMessage = "Il Codice Cliente è obbligatorio.")]
    [StringLength(20, ErrorMessage = "Il Codice Cliente non può superare i 20 caratteri.")]
    public string CodiceCliente { get; set; } = default!;

    [Required(ErrorMessage = "Specificare se il cliente è fidelizzato.")]
    public bool IsFidelizzato { get; set; }

    [Required(ErrorMessage = "Specificare se il cliente è iscritto alla newsletter.")]
    public bool IsIscrittoNewsletter { get; set; }
}