using System.ComponentModel.DataAnnotations;
namespace store.core._.Application.Dto.Person;

public class PersonDeleteRequest
{
    [Required(ErrorMessage = "Il codice fiscale è obbligatorio.")]
    [StringLength(16, MinimumLength = 16, ErrorMessage = "Il codice fiscale deve essere di 16 caratteri.")]
    public string CodiceFiscale { get; set; } = default!;
}