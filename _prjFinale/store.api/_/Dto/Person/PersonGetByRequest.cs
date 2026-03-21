using System.ComponentModel.DataAnnotations;
namespace store.api._.Dto.Person;

public class PersonGetByRequest
{
    [Required(ErrorMessage = "Il codice fiscale è obbligatorio.")]
    [StringLength(16, MinimumLength = 16, ErrorMessage = "Il codice fiscale deve essere di 16 caratteri.")]
    public string CodiceFiscale { get; set; } = string.Empty;
}