using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Employee;

public class EmployeeUpdateRequest
{
    // Attualmente 'PersonId' non è modificabile
    /* [Required(ErrorMessage = "L'ID della persona associata è obbligatorio.")]
    public int PersonId { get; set; } */
    
    // 'CodiceMeccanografico' usato ai fini di verifica esistenza
    [Required(ErrorMessage = "Il Codice Meccanografico è obbligatorio.")]
    [StringLength(10, ErrorMessage = "Il Codice Meccanografico non può superare i 10 caratteri.")]
    public string CodiceMeccanografico { get; set; } = default!;

    [Required(ErrorMessage = "L'Email aziendale è obbligatoria.")]
    [EmailAddress(ErrorMessage = "Formato Email aziendale non valido.")]
    [StringLength(255, ErrorMessage = "L'Email aziendale non può superare i 255 caratteri.")]
    public string EmailAziendale { get; set; } = default!;

    [Required(ErrorMessage = "La Password è obbligatoria.")]
    [StringLength(24, MinimumLength = 8, ErrorMessage = "La Password deve essere compresa tra 8 e 24 caratteri.")]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "Il Ruolo è obbligatorio.")]
    [StringLength(50, ErrorMessage = "Il Ruolo non può superare i 50 caratteri.")]
    public string Ruolo { get; set; } = default!;

    [Required(ErrorMessage = "Il Salario è obbligatorio.")]
    [Range(0, 99999999.99, ErrorMessage = "Il Salario deve essere un valore positivo.")]
    public decimal Salario { get; set; }
}