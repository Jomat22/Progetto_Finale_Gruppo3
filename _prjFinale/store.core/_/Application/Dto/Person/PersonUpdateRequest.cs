using System.ComponentModel.DataAnnotations;
namespace store.core._.Application.Dto.Person;

public class PersoUpdateRequest
{
    // Commentato perché specifico dell'admin non ancora implementato
    /* [Required(ErrorMessage = "Il Codice Fiscale è obbligatorio.")]
    [StringLength(16, MinimumLength = 16, ErrorMessage = "Il Codice Fiscale deve essere di 16 caratteri.")]
    public string CodiceFiscale { get; set; } = default!;

    [Required(ErrorMessage = "Il Nome è obbligatorio.")]
    [StringLength(100, ErrorMessage = "Il Nome non può superare i 100 caratteri.")]
    public string Nome { get; set; } = default!;

    [Required(ErrorMessage = "Il Cognome è obbligatorio.")]
    [StringLength(100, ErrorMessage = "Il Cognome non può superare i 100 caratteri.")]
    public string Cognome { get; set; } = default!;

    [Required(ErrorMessage = "Il Sesso è obbligatorio.")]
    [RegularExpression("^[MF]$", ErrorMessage = "Il Sesso deve essere 'M' o 'F'.")]
    public string Sesso { get; set; } = default!;

    [Required(ErrorMessage = "La Data di Nascita è obbligatoria.")]
    public DateOnly DataNascita { get; set; } */

    [Required(ErrorMessage = "La Città è obbligatoria.")]
    [StringLength(100, ErrorMessage = "La Città non può superare i 100 caratteri.")]
    public string Citta { get; set; } = default!;

    [Required(ErrorMessage = "La Provincia è obbligatoria.")]
    [StringLength(5, ErrorMessage = "La Provincia non può superare i 5 caratteri.")]
    public string Provincia { get; set; } = default!;

    [Required(ErrorMessage = "Il CAP è obbligatorio.")]
    [StringLength(10, ErrorMessage = "Il CAP non può superare i 10 caratteri.")]
    public string CodicePostale { get; set; } = default!;

    [Required(ErrorMessage = "L'Indirizzo è obbligatorio.")]
    [StringLength(255, ErrorMessage = "L'Indirizzo non può superare i 255 caratteri.")]
    public string Indirizzo { get; set; } = default!;

    [Required(ErrorMessage = "Il numero di contatto è obbligatorio.")]
    [StringLength(20, ErrorMessage = "Il numero non può superare i 20 caratteri.")]
    [Phone(ErrorMessage = "Formato numero di telefono non valido.")]
    public string NumeroContatto { get; set; } = default!;

    [Required(ErrorMessage = "L'Email è obbligatoria.")]
    [StringLength(255, ErrorMessage = "L'Email non può superare i 255 caratteri.")]
    [EmailAddress(ErrorMessage = "Formato Email non valido.")]
    public string Email { get; set; } = default!;
}