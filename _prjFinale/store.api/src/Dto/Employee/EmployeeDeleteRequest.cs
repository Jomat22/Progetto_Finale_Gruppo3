using System.ComponentModel.DataAnnotations;
namespace store.api.src.Dto.Employee;

public class EmployeeDeleteRequest
{
    [Required(ErrorMessage = "Il Codice Meccanografico è obbligatorio.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Il Codice Meccanografico deve avere una lunghezza compresa tra 3 e 20 caratteri.")]
    public string CodiceMeccanografico { get; set; } = default!;
}